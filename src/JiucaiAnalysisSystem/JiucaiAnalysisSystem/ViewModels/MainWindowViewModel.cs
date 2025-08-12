using System;
using CommunityToolkit.Mvvm.Input;
using JiucaiAnalysisSystem.Core.HttpManage;

namespace JiucaiAnalysisSystem.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        PopCardCommand = new RelayCommand(async () =>
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            await HttpManage.GetHistoryForDate(date:date);
        });
    }

    /// <summary>
    /// 进入业务模块
    /// </summary>
    public RelayCommand PopCardCommand { get; }
}