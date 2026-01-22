using System.Net;
using UntisAPI.ResourceTypes;

namespace UntisAPI;

public class UntisClient : IDisposable
{
    public bool Authenticated => _token is not null;
    public Student Student;
    public Class Class;

    private readonly HttpClient _client;
    private readonly HttpClientHandler _handler;
    private readonly string _apiUrl = "https://hhgym.webuntis.com/WebUntis";
    private string? _token;

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
        foreach (Cookie c in _handler.CookieContainer.GetAllCookies())
        {
            Console.WriteLine($"{c.Name}: {c.Value}");
        }
        tokenResponse.EnsureSuccessStatusCode();
        _token = await tokenResponse.Content.ReadAsStringAsync();
    }

    private async Task fetchIdentity()
    {
        HttpResponseMessage identityResponse = await _client.GetAsync(
            "{_apiUrl}/api/rest/view/v1/timetable/filter?resourceType=STUDENT"
        );
        // TODO:
    }

    public async Task<List<List<Entry>>> GetTimetableAsync(DateTime start, DateTime end)
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
        // TODO:
    }

    public void Dispose()
    {
        _client.Dispose();
        _handler.Dispose();
    }
}
