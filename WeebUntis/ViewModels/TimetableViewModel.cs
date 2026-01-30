using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using UntisAPI.ResourceTypes;

namespace WeebUntis.ViewModels;

public partial class TimetableViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<DayData> _days = [];

    [ObservableProperty]
    private double _pixelsPerMinute = 2.0;

    [ObservableProperty]
    private TimeSpan _dayStartTime = new(7, 0, 0);

    public TimetableViewModel(TimeTable timetable)
    {
        foreach (Day day in timetable.Days)
        {
            List<PositionedLesson> dayLessons = [];

            foreach (Lesson lesson in day.Lessons)
            {
                Console.WriteLine(lesson.Subject?.Current?.DisplayName);
                dayLessons.Add(new PositionedLesson(lesson, _dayStartTime, _pixelsPerMinute));
            }

            Days.Add(new DayData(dayLessons));
        }
    }
}

public class DayData(List<PositionedLesson> lessons)
{
    public List<PositionedLesson> Lessons { get; } = lessons;
    public double MaxHeight =>
        Lessons.DefaultIfEmpty().Max(l => l!.HeightInPixels + l.TopPositionInPixels);
}

public class PositionedLesson
{
    public Lesson Lesson { get; }
    public double TopPositionInPixels { get; }
    public double HeightInPixels { get; }

    public PositionedLesson(Lesson lesson, TimeSpan dayStartTime, double pixelsPerMinute)
    {
        Lesson = lesson;

        TimeSpan startOffset = lesson.Duration.Start.TimeOfDay - dayStartTime;
        double startMinutes = startOffset.TotalMinutes;

        TimeSpan duration = lesson.Duration.End - lesson.Duration.Start;
        double durationMinutes = duration.TotalMinutes;

        TopPositionInPixels = Math.Max(0, startMinutes * pixelsPerMinute);
        HeightInPixels = Math.Max(20, durationMinutes * pixelsPerMinute);
    }
}
