using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JiucaiAnalysisSystem.Common;
using JiucaiAnalysisSystem.Common.Utilities;
using JiucaiAnalysisSystem.Core.DB;
using JiucaiAnalysisSystem.Services;
using JiucaiAnalysisSystem.ViewModels;
using JiucaiAnalysisSystem.Views;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Dialogs;
using SukiUI.Toasts;

namespace JiucaiAnalysisSystem;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Load();
    }

    private void Load()
    {
        ConfigManager.Initialization();
        MySqlDb.InitDb();
        // new MySqlDb().CheckAndCreateDatabase();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            var services = new ServiceCollection();
            services.AddSingleton(desktop);
            var views = ConfigureViews(services);
            var provider = ConfigureServices(services);
            DataTemplates.Add(new ViewLocator(views));
            desktop.MainWindow = views.CreateView<MainWindowViewModel>(provider) as Window;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private static SukiViews ConfigureViews(ServiceCollection services)
    {
        return new SukiViews()

            // Add main view
            .AddView<MainWindow, MainWindowViewModel>(services)
            .AddView<DailyStocksView, DailyStocksViewModel>(services);
    }

    private static ServiceProvider ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<ClipboardService>();
        services.AddSingleton<PageNavigationService>();
        services.AddSingleton<ISukiToastManager, SukiToastManager>();
        services.AddSingleton<ISukiDialogManager, SukiDialogManager>();

        return services.BuildServiceProvider();
    }
}