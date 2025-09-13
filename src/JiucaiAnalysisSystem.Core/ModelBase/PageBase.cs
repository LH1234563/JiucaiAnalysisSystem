using Material.Icons;
using ReactiveUI;

namespace JiucaiAnalysisSystem.Core.ModelBase;

public abstract class PageBase(string displayName, MaterialIconKind icon, int index = 0) : ReactiveObject
{
    /// <summary>
    /// 
    /// </summary>
    private string _displayName = displayName;

    public string DisplayName
    {
        get => _displayName;
        set => this.RaiseAndSetIfChanged(ref _displayName, value);
    }

    private MaterialIconKind _icon = icon;

    public MaterialIconKind Icon
    {
        get => _icon;
        set => this.RaiseAndSetIfChanged(ref _icon, value);
    }

    private int _index = index;

    public int Index
    {
        get => _index;
        set => this.RaiseAndSetIfChanged(ref _index, value);
    }
}