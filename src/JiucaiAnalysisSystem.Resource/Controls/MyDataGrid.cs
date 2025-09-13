using System.Collections;
using Avalonia;
using Avalonia.Controls;

namespace JiucaiAnalysisSystem.Resource.Controls;

public class MyDataGrid : DataGrid
{
    public static readonly StyledProperty<IEnumerable> PageNumbersProperty =
        AvaloniaProperty.Register<MyDataGrid, IEnumerable>(nameof(PageNumbers));

    public IEnumerable PageNumbers
    {
        get => GetValue(PageNumbersProperty);
        set => SetValue(PageNumbersProperty, value);
    }
    public static readonly StyledProperty<int> CurrentPageProperty =
        AvaloniaProperty.Register<MyDataGrid, int>(nameof(CurrentPage));
    
    public int CurrentPage
    {
        get => GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, value);
    }
}