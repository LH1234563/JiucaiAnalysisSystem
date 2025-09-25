using JiucaiAnalysisSystem.Core.ModelBase;
using Material.Icons;
using SukiUI.Dialogs;
using SukiUI.Toasts;

namespace JiucaiAnalysisSystem.ViewModels;

public class HistoryStocksViewModel : PageBase
{
    private ISukiToastManager ToastManager { get; }
    private ISukiDialogManager DialogManager { get; }

    public HistoryStocksViewModel(ISukiToastManager toastManager, ISukiDialogManager dialogManager) : base("历史数据",
        MaterialIconKind.PaletteOutline, 1)
    {
        ToastManager = toastManager;
        DialogManager = dialogManager;
    }
}