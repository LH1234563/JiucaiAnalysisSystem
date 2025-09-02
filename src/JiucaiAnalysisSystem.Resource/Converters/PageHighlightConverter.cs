using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace JiucaiAnalysisSystem.Resource.Converters;

public class PageHighlightConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int page && parameter is int currentPage)
        {
            return page == currentPage ? Brushes.LightBlue : Brushes.Transparent;
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}