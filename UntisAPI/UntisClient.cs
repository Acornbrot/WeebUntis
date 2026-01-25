using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using UntisAPI.Json;
using UntisAPI.ResourceTypes;

namespace UntisAPI;

public class UntisClient : IDisposable
{
    public bool Authenticated => _token is not null;
    public Student Student
    {
        get
        {
            if (!Authenticated)
            {
                throw new InvalidOperationException("Client not authenticated.");
            }
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
            if (!Authenticated)
            {
                throw new InvalidOperationException("Client not authenticated.");
            }
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
    private readonly string _apiUrl = "https://hhgym.webuntis.com/WebUntis";
    private string? _token;

    private Student? _student;
    private List<Class>? _classes;

    public UntisClient()
    {
        _handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseCookies = true,
            CookieContainer = new CookieContainer(),
        };

        _client = new HttpClient(_handler);
    }

    public async Task AuthenticateAsync(string username, string password)
    {
        // get JSESSIONID cookie
        HttpResponseMessage initialResponse = await _client.GetAsync(_apiUrl);
        initialResponse.EnsureSuccessStatusCode();

        // register session id on the server
        Dictionary<string, string> authRequestBody = new()
        {
            { "school", "hhgym" },
            { "j_username", username },
            { "j_password", password },
        };

        FormUrlEncodedContent content = new(authRequestBody);
        HttpResponseMessage authResponse = await _client.PostAsync(
            $"{_apiUrl}/j_spring_security_check",
            content
        );
        authResponse.EnsureSuccessStatusCode();

        // retrieve token
        HttpResponseMessage tokenResponse = await _client.GetAsync($"{_apiUrl}/api/token/new");
        tokenResponse.EnsureSuccessStatusCode();
        _token = await tokenResponse.Content.ReadAsStringAsync();

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
        if (!Authenticated)
        {
            throw new InvalidOperationException(
                "Client not authenticated. Authenticate using the AuthenticateAsync method."
            );
        }

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
