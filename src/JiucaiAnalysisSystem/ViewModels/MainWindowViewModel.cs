using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using JiucaiAnalysisSystem.Common.Entity;
using JiucaiAnalysisSystem.Common.Utilities;
using JiucaiAnalysisSystem.Core.DB;
using JiucaiAnalysisSystem.Core.HttpManage;
using ReactiveUI;

namespace JiucaiAnalysisSystem.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        LoadedCommand = ReactiveCommand.Create(Load);
        UpdateDataCommand = ReactiveCommand.Create<object?, Task>(OnUpdateData);
    }

    private async Task OnUpdateData(object? obj)
    {
        try
        {
            if (obj is DateTimeOffset date)
            {
                var eastMoneyStocks = await HttpManage.GetHistoryForDate(date.ToString("yyyyMMdd"));
                var result = await MySqlDb.Db.Insertable(eastMoneyStocks).ExecuteCommandAsync();
            }

            Refresh();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
        }
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
        Refresh();
    }

    private void Refresh()
    {
        try
        {
            var result = MySqlDb.Db.Queryable<EastMoneyStock>().ToList();
            EastMoneyStocks.AddRange(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public ReactiveCommand<Unit, Task> LoadedCommand { get; }
    public ReactiveCommand<object?, Task> UpdateDataCommand { get; }

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

    private ObservableCollection<EastMoneyStock> _eastMoneyStocks = new();

    /// <summary>
    /// 自定义背景图
    /// </summary>
    public ObservableCollection<EastMoneyStock> EastMoneyStocks
    {
        get => _eastMoneyStocks;
        set => this.RaiseAndSetIfChanged(ref _eastMoneyStocks, value);
    }
}