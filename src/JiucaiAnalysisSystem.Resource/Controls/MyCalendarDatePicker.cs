using Avalonia;
using Avalonia.Controls;
using SqlSugar;

namespace JiucaiAnalysisSystem.Resource.Controls;

public class MyCalendarDatePicker : CalendarDatePicker
{
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property == DateTimesItemsSourceProperty)
        {
            try
            {

                IList<DateTime> dateTimes = (IList<DateTime>)change.NewValue;
                // var dateTimes = change.NewValue;
                if (dateTimes == null ||
                    dateTimes.Count < 0)
                    return;
                // 为每一个允许的日期创建一个单日的 CalendarDateRange
                var startTime = dateTimes.First();
                var endTime = dateTimes.Last();
                while (startTime < endTime)
                {
                    startTime = startTime.AddDays(1);
                    if (dateTimes.Contains(startTime))
                    {
                        continue;
                    }

                    CalendarDateRange allowedRange = new CalendarDateRange(startTime);
                    // 从 BlackoutDates 中移除这个范围
                    BlackoutDates?.Add(allowedRange);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        base.OnPropertyChanged(change);
    }

    public static readonly StyledProperty<IList<DateTime>> DateTimesItemsSourceProperty =
        AvaloniaProperty.Register<MyCalendarDatePicker, IList<DateTime>>(nameof(DateTimesItemsSource));

    /// <summary>
    /// Gets or sets a collection that is used to generate the content of the control.
    /// </summary>
    public IList<DateTime> DateTimesItemsSource
    {
        get => GetValue(DateTimesItemsSourceProperty);
        set => SetValue(DateTimesItemsSourceProperty, value);
    }
}