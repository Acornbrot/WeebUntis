using System.Net;

namespace UntisAPI;

public class UntisClient : IDisposable
{
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

    public void Dispose()
    {
        _client.Dispose();
        _handler.Dispose();
    }
}
