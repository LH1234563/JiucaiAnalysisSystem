using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia.Xaml.Interactions.Custom;
using JiucaiAnalysisSystem.Common.Entity;
using JiucaiAnalysisSystem.Common.Utilities;
using JiucaiAnalysisSystem.Core.HttpManage;
using JiucaiAnalysisSystem.Core.ModelBase;
using JiucaiAnalysisSystem.Services;
using ReactiveUI;
using SukiUI.Dialogs;
using SukiUI.Toasts;

namespace JiucaiAnalysisSystem.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    public IAvaloniaReadOnlyList<PageBase> Pages { get; }

    public MainWindowViewModel(IEnumerable<PageBase> pages, PageNavigationService pageNavigationService,
        ISukiToastManager toastManager, ISukiDialogManager dialogManager)
    {
        Pages = new AvaloniaList<PageBase>(pages.OrderBy(x => x.Index).ThenBy(x => x.DisplayName));
        LoadedCommand = ReactiveCommand.Create(Load);
    }

    public ReactiveCommand<Unit, Task> LoadedCommand { get; }

    private async Task Load()
    {
        await Task.Delay(1);
    }

    /// <summary>
    /// 
    /// </summary>
    private PageBase? _activePage;

    public PageBase? ActivePage
    {
        get => _activePage;
        set => this.RaiseAndSetIfChanged(ref _activePage, value);
    }
}