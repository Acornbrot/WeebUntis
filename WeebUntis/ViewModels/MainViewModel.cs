using System;
using CommunityToolkit.Mvvm.ComponentModel;
using UntisAPI.ResourceTypes;

namespace WeebUntis.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";

    [ObservableProperty]
    private Lesson _lesson;

    public MainViewModel()
    {
        _lesson = new Lesson()
        {
            Color = "#ff0000",
            Duration = new Duration
            {
                Start = DateTimeOffset.Now,
                End = DateTimeOffset.Now.AddMinutes(67),
            },
            LayoutGroup = 0,
            LayoutStartPosition = 0,
            LayoutWidth = 1000,
            Status = UntisStatus.Regular,
            Type = LessonType.NormalTeaching,
            Teachers =
            [
                new PositionEntry<Teacher>()
                {
                    Current = new Teacher()
                    {
                        DisplayName = "Johannes Meister",
                        LongName = "Johannes Meister",
                        ShortName = "MEI",
                        Status = UntisStatus.Removed,
                    },
                    Removed = new Teacher()
                    {
                        DisplayName = "Sabrina Mertins",
                        LongName = "Sabrina Mertins",
                        ShortName = "MER",
                        Status = UntisStatus.Cancelled,
                    },
                },
            ],
            Rooms =
            [
                new PositionEntry<Room>()
                {
                    Removed = new Room()
                    {
                        DisplayName = "Strahlenschutzbunker (STRB)",
                        LongName = "Strahlenschutzbunker der Reiterstaffel",
                        ShortName = "STRB",
                        Status = UntisStatus.Regular,
                    },
                },
            ],
            Subjects =
            [
                new PositionEntry<Subject>()
                {
                    Current = new Subject()
                    {
                        DisplayName = "Mathsmaxxing",
                        LongName = "Mathsmaxxing",
                        ShortName = "MA",
                        Status = UntisStatus.Regular,
                    },
                },
            ],
        };
    }
}
