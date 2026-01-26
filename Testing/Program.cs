using System.Globalization;
using System.Text.Json;
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

UntisClient client = await UntisClient.CreateAsync(
    username,
    password,
    "https://hhgym.webuntis.com/WebUntis"
);

DateTimeOffset now = DateTimeOffset.Now;
int diff = (7 + (now.DayOfWeek - DayOfWeek.Saturday)) % 7;
DateTimeOffset weekStart = now.AddDays(-diff + 2).Date;
DateTimeOffset weekEnd = weekStart.AddDays(5);

TimeTable timeTable = await client.GetTimetableAsync(weekStart, weekEnd);

string json = JsonSerializer.Serialize(
    timeTable,
    new JsonSerializerOptions { WriteIndented = true }
);

List<Lesson> ausfaelle =
[
    .. timeTable.Days.SelectMany(d =>
        d.Lessons.Where(l =>
            l is { Status: UntisStatus.Changed, Teacher: { Current: null, Removed: not null } }
        )
    ),
];

Console.WriteLine("# Ausfälle:");

foreach (Lesson ausfall in ausfaelle)
{
    Console.WriteLine();
    Console.WriteLine($"Fach: {ausfall.Subject!.Current.DisplayName}");
    Console.WriteLine($"Lehrer: {ausfall.Teacher!.Removed!.DisplayName}");
    Console.WriteLine($"Raum: {ausfall.Room!.Current.DisplayName}");
    Console.WriteLine(
        $"Uhrzeit: {ausfall.Duration.Start.ToString("o", CultureInfo.InvariantCulture)} - {ausfall.Duration.End.ToString("O", CultureInfo.InvariantCulture)}"
    );
}
