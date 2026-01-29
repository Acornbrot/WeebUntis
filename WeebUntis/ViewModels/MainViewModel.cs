using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UntisAPI;

namespace WeebUntis.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private TimetableViewModel? _timeTable;

    public MainViewModel()
    {
        _ = loadTimetableAsync();
    }

    private async Task loadTimetableAsync()
    {
        Console.Write("Untis user: ");
        string user = Console.ReadLine()!;
        Console.Write("Untis password: ");
        string password = Console.ReadLine()!;

        UntisClient _client = await UntisClient.CreateAsync(
            user,
            password,
            "https://hhgym.webuntis.com/WebUntis"
        );

        DateTimeOffset now = DateTimeOffset.Now;
        int diff = (7 + (now.DayOfWeek - DayOfWeek.Saturday)) % 7;
        DateTimeOffset weekStart = now.AddDays(-diff + 2).Date;
        DateTimeOffset weekEnd = weekStart.AddDays(4);

        TimeTable = new TimetableViewModel(await _client.GetTimetableAsync(weekStart, weekEnd));
    }
}
