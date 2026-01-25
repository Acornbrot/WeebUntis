using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using UntisAPI;
using UntisAPI.ResourceTypes;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

string username =
    config["Untis:Username"]
    ?? throw new InvalidOperationException("Please add Untis:Username to the appsettings");
string password =
    config["Untis:Password"]
    ?? throw new InvalidOperationException("Please add Untis:Password to the appsettings");

UntisClient client = new();
await client.AuthenticateAsync(username, password);

DateTimeOffset now = DateTimeOffset.Now;
int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
DateTimeOffset weekStart = now.AddDays(-diff).Date;
DateTimeOffset weekEnd = weekStart.AddDays(5);

TimeTable timeTable = await client.GetTimetableAsync(weekStart, weekEnd);

string json = JsonSerializer.Serialize(
    timeTable,
    new JsonSerializerOptions { WriteIndented = true }
);
