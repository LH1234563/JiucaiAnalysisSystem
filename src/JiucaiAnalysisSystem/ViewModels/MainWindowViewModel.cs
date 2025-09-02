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
        LoadedCommand = ReactiveCommand.Create(Loaded);
        RefreshDataCommand = ReactiveCommand.Create(OnRefreshData);
        SelectedDateChangedCommand = ReactiveCommand.Create(OnRefreshData);
    }

    #region Method

    private async Task OnRefreshData()
    {
        try
        {
            bool anyStock = await MySqlDb.Db.Queryable<EastMoneyStock>()
                .Where(it => it.CurrentDate == SelectedDate.ToString("yyyyMMdd")).CountAsync() > 5000;
            if (!anyStock)
            {
                Tip = "正在更新数据";
                var eastMoneyStocks = await HttpManage.GetHistoryForDate(SelectedDate.ToString("yyyyMMdd"));
                await MySqlDb.Db.Insertable(eastMoneyStocks).ExecuteCommandAsync();
            }

            Refresh();
            Tip = null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
        }
    }

    private async Task Loaded()
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

        // Codes.AddRange(ConfigManager.StockCodeAll);
        await OnRefreshData();
    }

    private void Refresh()
    {
        try
        {
            var result = MySqlDb.Db.Queryable<EastMoneyStock>()
                .Where(it => it.CurrentDate == SelectedDate.ToString("yyyyMMdd")).ToList();

            EastMoneyStocks.Clear();
            EastMoneyStocks.AddRange(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Task> LoadedCommand { get; }
    public ReactiveCommand<Unit, Task> SelectedDateChangedCommand { get; }
    public ReactiveCommand<Unit, Task> RefreshDataCommand { get; }

    #endregion

    #region UIBinding

    private string? _tip;

    public string? Tip
    {
        get => _tip;
        set => this.RaiseAndSetIfChanged(ref _tip, value);
    }

    /// <summary>
    /// 进入业务模块
    /// </summary>
    private DateTimeOffset _selectedDate = DateTimeOffset.Now;

    public DateTimeOffset SelectedDate
    {
        get => _selectedDate;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedDate, value);
            Refresh();
        }
    }


    /// <summary>
    /// 所有股票代码
    /// </summary>
    // private ObservableCollection<string> _codes = new();
    //
    // public ObservableCollection<string> Codes
    // {
    //     get => _codes;
    //     set => this.RaiseAndSetIfChanged(ref _codes, value);
    // }

    /// <summary>
    /// 所有股票
    /// </summary>
    private ObservableCollection<EastMoneyStock> _eastMoneyStocks = new();

    public ObservableCollection<EastMoneyStock> EastMoneyStocks
    {
        get => _eastMoneyStocks;
        set => this.RaiseAndSetIfChanged(ref _eastMoneyStocks, value);
    }

    #endregion
}