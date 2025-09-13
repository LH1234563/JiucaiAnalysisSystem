using System;
using JiucaiAnalysisSystem.Core.ModelBase;

namespace JiucaiAnalysisSystem.Services;


public class PageNavigationService
{
    public Action<Type>? NavigationRequested { get; set; }

    public void RequestNavigation<T>() where T : PageBase
    {
        NavigationRequested?.Invoke(typeof(T));
    }
}