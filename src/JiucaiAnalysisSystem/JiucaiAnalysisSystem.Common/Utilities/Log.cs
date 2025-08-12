using System.Runtime.InteropServices;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace JiucaiAnalysisSystem.Common.Utilities;

public class Log
{
    public static readonly string PathString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

    public static Logger Logger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .WriteTo.Logger(lc => lc
            .WriteTo.File(Path.Combine(PathString, "Debug.log"), rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30, restrictedToMinimumLevel: LogEventLevel.Debug)
            .WriteTo.File(Path.Combine(PathString, "Info.log"), rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30, restrictedToMinimumLevel: LogEventLevel.Information)
            .WriteTo.File(Path.Combine(PathString, "Warning.log"), rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30, restrictedToMinimumLevel: LogEventLevel.Warning)
            .WriteTo.File(Path.Combine(PathString, "Error.log"), rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30, restrictedToMinimumLevel: LogEventLevel.Error)
        )
        .CreateLogger();
}