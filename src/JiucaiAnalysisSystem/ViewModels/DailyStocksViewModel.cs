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
using JiucaiAnalysisSystem.Core.ModelBase;
using Material.Icons;
using ReactiveUI;

namespace JiucaiAnalysisSystem.ViewModels;

public class DailyStocksViewModel() : PageBase("每日数据", MaterialIconKind.PaletteOutline, 1)
{
    public ReactiveCommand<Unit, Task> UpdateDataCommand => ReactiveCommand.Create(OnUpdateData);

    public ReactiveCommand<Unit, Task> LoadedCommand => ReactiveCommand.Create(Load);

    private async Task Load()
    {
        try
        {
            var dateTimes = await HttpManage.GetAllTradeDate();
            if (dateTimes.Count > 0)
            {
                SelectedDate = dateTimes.Last();
                TradeDates = new ObservableCollection<DateTime>(dateTimes);
                ConfigManager.TradeDates = dateTimes;
            }

            if (MeasureHelper.IsExistStockCodeAll())
            {
                return;
            }

            var stockCodeAll = await HttpManage.GetAllStockCodes();
            if (stockCodeAll.Count < 5000)
            {
                return;
            }

            ConfigManager.StockCodeAll = stockCodeAll;
        }
        catch (Exception e)
        {
            Log.Logger.Error(e.Message);
        }
    }

    private async Task OnUpdateData()
    {
        IsBusy = true;
        try
        {
            var eastMoneyStocks = await HttpManage.GetHistoryForDate(SelectedDate.ToString("yyyyMMdd"));
            var result = await MySqlDb.Db.Insertable(eastMoneyStocks).ExecuteCommandAsync();
            Refresh();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void Refresh()
    {
        try
        {
            var result = MySqlDb.Db.Queryable<EastMoneyStock>()
                .Where(a => a.CurrentDate.Equals(SelectedDate.ToString("yyyyMMdd"))).ToList();
            EastMoneyStocks.AddRange(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }

    /// <summary>
    /// 
    /// </summary>
    private DateTime _selectedDate;

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (_selectedDate != value)
            {
                Refresh();
            }

            this.RaiseAndSetIfChanged(ref _selectedDate, value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private ObservableCollection<DateTime> _tradeDates;

    public ObservableCollection<DateTime> TradeDates
    {
        get => _tradeDates;
        set => this.RaiseAndSetIfChanged(ref _tradeDates, value);
    }

    private ObservableCollection<EastMoneyStock> _eastMoneyStocks = new();

    /// <summary>
    /// 
    /// </summary>
    public ObservableCollection<EastMoneyStock> EastMoneyStocks
    {
        get => _eastMoneyStocks;
        set => this.RaiseAndSetIfChanged(ref _eastMoneyStocks, value);
    }
}