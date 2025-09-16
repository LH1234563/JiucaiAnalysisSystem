using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using JiucaiAnalysisSystem.Common.Utilities;

namespace JiucaiAnalysisSystem.Views;

public partial class DailyStocksView : UserControl
{
    public DailyStocksView()
    {
        InitializeComponent();
    }


    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (sender is CalendarDatePicker calendarDatePicker && ConfigManager.TradeDates.Count > 0)
        {
            // 为每一个允许的日期创建一个单日的 CalendarDateRange
            var startTime = ConfigManager.TradeDates.First();
            var endTime = ConfigManager.TradeDates.Last();
            calendarDatePicker.DisplayDateStart = ConfigManager.TradeDates.First();
            calendarDatePicker.DisplayDateEnd = ConfigManager.TradeDates.Last();
            while (startTime < endTime)
            {
                startTime = startTime.AddDays(1);
                if (ConfigManager.TradeDates.Contains(startTime))
                {
                    continue;
                }

                CalendarDateRange allowedRange = new CalendarDateRange(startTime);
                // 从 BlackoutDates 中移除这个范围
                calendarDatePicker.BlackoutDates.Add(allowedRange);
            }
        }
    }
}