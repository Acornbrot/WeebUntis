using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using UntisAPI.Json;
using UntisAPI.ResourceTypes;

namespace UntisAPI;

public sealed class UntisClient : IDisposable
{
    public Student Student
    {
        get
        {
            if (_student is null)
            {
                throw new InvalidOperationException(
                    "Student was null (this shouldn't ever happen)"
                );
            }
            return _student;
        }
        private set => _student = value;
    }
    public List<Class> Classes
    {
        get
        {
            if (_classes is null)
            {
                throw new InvalidOperationException(
                    "Classes was null (this shouldn't ever happen)"
                );
            }
            return _classes;
        }
        private set => _classes = value;
    }

    private readonly HttpClient _client;
    private readonly HttpClientHandler _handler;
    private readonly string _apiUrl;
    private string _token;

    private Student _student;
    private List<Class> _classes;

    // The warning can safely be ignored because the static
    // factory method will ensure that the instance has valid properties
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private UntisClient(string apiUrl)
    {
        _handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseCookies = true,
            CookieContainer = new CookieContainer(),
        };

        _client = new HttpClient(_handler);
        _apiUrl = apiUrl.TrimEnd('/');
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public static async Task<UntisClient> CreateAsync(string user, string password, string apiUrl)
    {
        UntisClient client = new(apiUrl);
        await client.AuthenticateAsync(user, password);
        return client;
    }

    public async Task AuthenticateAsync(string username, string password)
    {
        // get JSESSIONID cookie
        HttpResponseMessage initialResponse = await _client.GetAsync(_apiUrl);
        initialResponse.EnsureSuccessStatusCode();

        // register session id on the server
        Dictionary<string, string> authRequestBody = new()
        {
            { "j_username", username },
            { "j_password", password },
        };

        FormUrlEncodedContent content = new(authRequestBody);
        HttpResponseMessage authResponse = await _client.PostAsync(
            $"{_apiUrl}/j_spring_security_check",
            content
        );
        authResponse.EnsureSuccessStatusCode();

        // j_spring_security_check returns 200 regardless of the credentials
        // so I wrap the token request in a try catch

        // retrieve token
        HttpResponseMessage tokenResponse = await _client.GetAsync($"{_apiUrl}/api/token/new");
        tokenResponse.EnsureSuccessStatusCode();
        _token = await tokenResponse.Content.ReadAsStringAsync();

        // For some reason Untis returns an html document when the client is not authenticated
        if (_token.Contains("\n"))
        {
            throw new ArgumentException("Invalid credentials");
        }

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            _token
        );

        await fetchIdentity();
    }

    private async Task fetchIdentity()
    {
        HttpResponseMessage identityResponse = await _client.GetAsync(
            $"{_apiUrl}/api/rest/view/v1/timetable/filter?resourceType=STUDENT"
        );
        identityResponse.EnsureSuccessStatusCode();

        string json = await identityResponse.Content.ReadAsStringAsync();

        JsonSerializerSettings settings = new() { Converters = [new IdentityConverter()] };
        Identity identity =
            JsonConvert.DeserializeObject<Identity>(json, settings)
            ?? throw new InvalidDataException(
                "Found unexpected json object while fetching identity"
            );

        _student = identity.Student;
        _classes = identity.Classes;
    }

    public async Task<TimeTable> GetTimetableAsync(DateTimeOffset start, DateTimeOffset end)
    {
        HttpResponseMessage timetableResponse = await _client.GetAsync(
            $"{_apiUrl}/api/rest/view/v1/timetable/entries?start={start.Date:yyyy-MM-dd}&end={end.Date:yyyy-MM-dd}&resourceType=STUDENT&resources={Student.Id}"
        );
        timetableResponse.EnsureSuccessStatusCode();

        string json = await timetableResponse.Content.ReadAsStringAsync();

        JsonSerializerSettings settings = new()
        {
            Converters = [new LessonTypeConverter(), new UntisStatusConverter()],
        };

        return JsonConvert.DeserializeObject<TimeTable>(json, settings)
            ?? throw new InvalidDataException(
                "Found unexpected json object while fetching timetable"
            );
    }

    public void Dispose()
    {
        _client.Dispose();
        _handler.Dispose();
    }
}
