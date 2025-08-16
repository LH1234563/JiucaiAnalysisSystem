using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using JiucaiAnalysisSystem.Common.Utilities;
using JiucaiAnalysisSystem.Core.HttpManage;
using ReactiveUI;

namespace JiucaiAnalysisSystem.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        LoadedCommand = ReactiveCommand.Create(Load);
    }

    private async Task Load()
    {
        if (!ConfigManager.StockCodeAll.Any())
        {
            Tip = "正在更新股票代码";
            ConfigManager.StockCodeAll = await HttpManage.GetAllStockCodes();
            if (ConfigManager.StockCodeAll.Any())
            {
                Tip = null;
            }
        }

        Codes.AddRange(ConfigManager.StockCodeAll);
    }

    public ReactiveCommand<Unit, Task> LoadedCommand { get; }

    /// <summary>
    /// 进入业务模块
    /// </summary>
    private string? _tip;

    /// <summary>
    /// 自定义背景图
    /// </summary>
    public string? Tip
    {
        get => _tip;
        set => this.RaiseAndSetIfChanged(ref _tip, value);
    }

    private ObservableCollection<string> _codes = new();

    /// <summary>
    /// 自定义背景图
    /// </summary>
    public ObservableCollection<string> Codes
    {
        get => _codes;
        set => this.RaiseAndSetIfChanged(ref _codes, value);
    }
}