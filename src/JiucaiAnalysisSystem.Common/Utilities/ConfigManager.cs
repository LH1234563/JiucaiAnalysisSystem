using JiucaiAnalysisSystem.Common.Entity;
using Newtonsoft.Json;

namespace JiucaiAnalysisSystem.Common.Utilities;

/// <summary>
/// 配置文件管理器
/// </summary>
public static class ConfigManager
{
    private static AppConfig _config;

    // 获取配置文件路径（默认在应用程序运行目录）
    private static string _configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
    public static List<DateTime> TradeDates = new List<DateTime>();


    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <returns>配置对象</returns>
    private static AppConfig? LoadConfig()
    {
        try
        {
            // 读取配置文件内容
            string jsonContent = File.ReadAllText(_configFilePath);

            // 反序列化为对象
            return JsonConvert.DeserializeObject<AppConfig>(jsonContent) ?? new AppConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"加载配置文件失败: {ex.Message}");
            // 发生错误时返回默认配置
            return null;
        }
    }

    /// <summary>
    /// 保存配置到文件
    /// </summary>
    /// <param name="config">配置对象</param>
    /// <returns>是否保存成功</returns>
    private static bool SaveConfig()
    {
        try
        {
            // 更新最后修改时间
            // config.LastUpdated = DateTime.Now;

            // 序列化对象为JSON字符串，格式化输出
            string jsonContent = JsonConvert.SerializeObject(_config, Formatting.Indented);

            // 写入文件
            File.WriteAllText(_configFilePath, jsonContent);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"保存配置文件失败: {ex.Message}");
            return false;
        }
    }

    public static bool Initialization()
    {
        var config = LoadConfig();
        if (config == null)
            return false;
        _config = config;
        return true;
    }

    /// <summary>
    /// 数据库服务器地址
    /// </summary>
    public static string DbHost
    {
        get => _config.DbHost;
        set
        {
            _config.DbHost = value;
            SaveConfig();
        }
    }

    /// <summary>
    /// 数据库用户名
    /// </summary>
    public static string DbUser
    {
        get => _config.DbUser;
        set
        {
            _config.DbUser = value;
            SaveConfig();
        }
    }

    /// <summary>
    /// 数据库密码
    /// </summary>
    public static string DbPassword
    {
        get => _config.DbPassword;
        set
        {
            _config.DbPassword = value;
            SaveConfig();
        }
    }

    /// <summary>
    /// 数据库名称
    /// </summary>
    public static string DbName
    {
        get => _config.DbName;
        set
        {
            _config.DbName = value;
            SaveConfig();
        }
    }

    /// <summary>
    /// 数据库端口
    /// </summary>
    public static int DbPort
    {
        get => _config.DbPort;
        set
        {
            _config.DbPort = value;
            SaveConfig();
        }
    }

    /// <summary>
    /// 交易日历压缩后的字符串，待访问不了新浪接口时使用
    /// </summary>
    public static string DateTimeEncodedCode
    {
        get => _config.DateTimeEncodedCode;
        set
        {
            _config.DateTimeEncodedCode = value;
            SaveConfig();
        }
    }

    /// <summary>
    /// 所有股票代码
    /// </summary>
    public static Dictionary<string, string>? StockCodeAll
    {
        get => _config.StockCodeAll;
        set
        {
            _config.StockCodeAll = value;
            SaveConfig();
        }
    }

    /// <summary>
    /// 所有股票代码
    /// </summary>
    // public static DateTime? SelectedDate
    // {
    //     get => _config.SelectedDate;
    //     set
    //     {
    //         _config.SelectedDate = value;
    //         SaveConfig();
    //     }
    // }
}