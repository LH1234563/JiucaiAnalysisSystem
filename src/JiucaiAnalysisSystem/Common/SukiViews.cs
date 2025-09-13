using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using JiucaiAnalysisSystem.Core.ModelBase;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace JiucaiAnalysisSystem.Common;

public class SukiViews
{
    
    private readonly Dictionary<Type, Type> _vmToViewMap = [];

    public SukiViews AddView<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TView,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>(ServiceCollection services)
        where TView : ContentControl
        where TViewModel : ReactiveObject
    {
        var viewType = typeof(TView);
        var viewModelType = typeof(TViewModel);

        _vmToViewMap.Add(viewModelType, viewType);

        if (viewModelType.IsAssignableTo(typeof(PageBase)))
        {
            services.AddSingleton(typeof(PageBase), viewModelType);
        }
        else
        {
            services.AddSingleton(viewModelType);
        }

        return this;
    }

    public bool TryCreateView(IServiceProvider provider, Type viewModelType, [NotNullWhen(true)] out Control? view)
    {
        var viewModel = provider.GetRequiredService(viewModelType);

        return TryCreateView(viewModel, out view);
    }

    public bool TryCreateView(object? viewModel, [NotNullWhen(true)] out Control? view)
    {
        view = null;

        if (viewModel == null)
        {
            return false;
        }

        var viewModelType = viewModel.GetType();

        if (_vmToViewMap.TryGetValue(viewModelType, out var viewType))
        {
            view = Activator.CreateInstance(viewType) as Control;

            if (view != null)
            {
                view.DataContext = viewModel;
            }
        }

        return view != null;
    }

    public Control CreateView<TViewModel>(IServiceProvider provider) where TViewModel : ReactiveObject
    {
        var viewModelType = typeof(TViewModel);

        if (TryCreateView(provider, viewModelType, out var view))
        {
            return view;
        }

        throw new InvalidOperationException();
    }
}