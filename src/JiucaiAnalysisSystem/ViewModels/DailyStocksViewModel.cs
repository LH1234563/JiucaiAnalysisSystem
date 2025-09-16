using System;
using System.Collections.ObjectModel;
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

    public ReactiveCommand<Unit, Unit> LoadedCommand => ReactiveCommand.Create(Load);

    private void Load()
    {
        if (ConfigManager.SelectedDate is null)
        {
            ConfigManager.SelectedDate = DateTime.Now.Date;
        }
        else
        {
            SelectedDate = ConfigManager.SelectedDate.Value;
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
    private DateTime _tradeDates;

    public DateTime TradeDates
    {
        get => _tradeDates;
        set => this.RaiseAndSetIfChanged(ref _tradeDates, value);
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