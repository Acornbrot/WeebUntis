using Microsoft.Extensions.Configuration;
using UntisAPI;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

string username =
    config["Untis:Username"] ?? throw new InvalidOperationException("Bad appsettings");
string password =
    config["Untis:Password"] ?? throw new InvalidOperationException("Bad appsettings");

UntisClient client = new();
await client.AuthenticateAsync(username, password);
Console.WriteLine($"Name: {client.Student.DisplayName}; Class: {client.Classes[0].DisplayName}");
