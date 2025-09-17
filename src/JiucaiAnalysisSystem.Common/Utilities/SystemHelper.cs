using Avalonia.Controls.ApplicationLifetimes;

namespace JiucaiAnalysisSystem.Common.Utilities;

public class SystemHelper
{
    public static void ShutdownApplication()
    {
        (Avalonia.Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Shutdown();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
}