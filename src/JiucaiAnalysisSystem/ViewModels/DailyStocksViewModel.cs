using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using DynamicData;
using JiucaiAnalysisSystem.Common.Entity;
using JiucaiAnalysisSystem.Common.Utilities;
using JiucaiAnalysisSystem.Core.DB;
using JiucaiAnalysisSystem.Core.HttpManage;
using JiucaiAnalysisSystem.Core.ModelBase;
using Material.Icons;
using ReactiveUI;
using SukiUI.Dialogs;
using SukiUI.Toasts;

namespace JiucaiAnalysisSystem.ViewModels;

public class DailyStocksViewModel : PageBase
{
    private ISukiToastManager ToastManager { get; }
    private ISukiDialogManager DialogManager { get; }

    public DailyStocksViewModel(ISukiToastManager toastManager, ISukiDialogManager dialogManager) : base("每日数据",
        MaterialIconKind.PaletteOutline, 1)
    {
        ToastManager = toastManager;
        DialogManager = dialogManager;
    }

    public ReactiveCommand<Unit, Task> UpdateDataCommand => ReactiveCommand.Create(OnUpdateData);
    // public ReactiveCommand<Unit, Task> UpdateHistoryDataCommand => ReactiveCommand.Create(OnUpdateHistoryData);

    // private Task OnUpdateHistoryData()
    // {
    //     while (expression)
    //     {
    //         
    //     }
    // }

    public ReactiveCommand<Unit, Task> LoadedCommand => ReactiveCommand.Create(Load);

    private async Task Load()
    {
        try
        {
            var dateTimes = await HttpManage.GetAllTradeDate();
            if (dateTimes.Count > 0)
            {
                TradeDates = new ObservableCollection<DateTime>(dateTimes);
                ConfigManager.TradeDates = dateTimes;
            }

            if (MeasureHelper.IsExistStockCodeAll())
            {
                SelectedDate = dateTimes.Last();
                return;
            }


            ToastManager.CreateToast()
                .WithTitle($"{NotificationType.Information}!")
                .WithContent("正在更新今日股票代码")
                .OfType(NotificationType.Information)
                .Dismiss().After(TimeSpan.FromSeconds(3))
                .Dismiss().ByClicking()
                .Queue();
            var stockCodeAll = await HttpManage.GetAllStockCodes();
            if (stockCodeAll.Count < 5000)
            {
                ToastManager.CreateToast()
                    .WithTitle($"{NotificationType.Error}!")
                    .WithContent(
                        $"今日股票代码更新失败")
                    .OfType(NotificationType.Error)
                    .Dismiss().After(TimeSpan.FromSeconds(3))
                    .Dismiss().ByClicking()
                    .Queue();
                return;
            }

            ToastManager.CreateToast()
                .WithTitle($"{NotificationType.Information}!")
                .WithContent(
                    $"今日股票代码更新成功，今日A股股票共：{stockCodeAll.Count}支")
                .OfType(NotificationType.Information)
                .Dismiss().After(TimeSpan.FromSeconds(3))
                .Dismiss().ByClicking()
                .Queue();
            ConfigManager.StockCodeAll = stockCodeAll;
            Refresh();
        }
        catch (Exception e)
        {
            Log.Logger.Error(e.Message);
        }
    }

    private async Task OnUpdateData()
    {
        try
        {
            ToastManager.CreateToast()
                .WithContent(
                    $"正在更新数据")
                .Dismiss().ByClicking()
                .Queue();
            var dataCount = ConfigManager.TradeDates.Count(a => a <= SelectedDate);
            var selectDataCount = await MySqlDb.Db.Queryable<EastMoneyStock>()
                .CountAsync(a => a.CurrentDate.Equals(SelectedDate.ToString("yyyyMMdd")));
            if (selectDataCount >= dataCount)
            {
                return;
            }

            var eastMoneyStocks = await HttpManage.GetHistoryForDate(SelectedDate.ToString("yyyyMMdd"));
            var result = await MySqlDb.Db.Insertable(eastMoneyStocks).ExecuteCommandAsync();
            if (result < dataCount)
            {
                ToastManager.CreateToast()
                    .WithTitle($"{NotificationType.Error}!")
                    .WithContent(
                        $"{SelectedDate:d}数据更新失败")
                    .OfType(NotificationType.Error)
                    .Dismiss().After(TimeSpan.FromSeconds(3))
                    .Dismiss().ByClicking()
                    .Queue();
                Console.WriteLine("更新数据失败");
                return;
            }

            Refresh();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
        }
    }

    private void Refresh()
    {
        try
        {
            EastMoneyStocks.Clear();
            var result = MySqlDb.Db.Queryable<EastMoneyStock>()
                .Where(a => a.CurrentDate.Equals(SelectedDate.ToString("yyyyMMdd"))).ToList();
            if (result == null || result.Count == 0)
            {
                ToastManager.CreateToast()
                    .WithTitle($"{NotificationType.Information}!")
                    .WithContent(
                        $"{SelectedDate:d}日的数据未更新")
                    .OfType(NotificationType.Information)
                    .Dismiss().After(TimeSpan.FromSeconds(3))
                    .Dismiss().ByClicking()
                    .Queue();
                return;
            }

            EastMoneyStocks.AddRange(result);
            ToastManager.CreateToast()
                .WithTitle($"{NotificationType.Success}!")
                .WithContent(
                    $"{SelectedDate:d}日的数据更新成功")
                .OfType(NotificationType.Success)
                .Dismiss().After(TimeSpan.FromSeconds(3))
                .Dismiss().ByClicking()
                .Queue();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ToastManager.CreateToast()
                .WithTitle($"{NotificationType.Error}!")
                .WithContent(
                    $"{SelectedDate:d}日的数据更新成功")
                .OfType(NotificationType.Success)
                .Dismiss().After(TimeSpan.FromSeconds(3))
                .Dismiss().ByClicking()
                .Queue();
        }
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
            var oldDate = _selectedDate;
            this.RaiseAndSetIfChanged(ref _selectedDate, value);
            if (_selectedDate != oldDate)
            {
                Refresh();
            }
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