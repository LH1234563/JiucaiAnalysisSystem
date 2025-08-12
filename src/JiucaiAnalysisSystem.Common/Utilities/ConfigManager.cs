using Newtonsoft.Json;

namespace JiucaiAnalysisSystem.Common.Utilities;

/// <summary>
/// 配置文件管理器
/// </summary>
public class ConfigManager
{
    // 获取配置文件路径（默认在应用程序运行目录）
    private static string _configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <returns>配置对象</returns>
    public static AppConfig LoadConfig()
    {
        try
        {
            // 如果配置文件不存在，则创建默认配置
            if (!File.Exists(_configFilePath))
            {
                return CreateDefaultConfig();
            }

            // 读取配置文件内容
            string jsonContent = File.ReadAllText(_configFilePath);

            // 反序列化为对象
            return JsonConvert.DeserializeObject<AppConfig>(jsonContent) ?? new AppConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"加载配置文件失败: {ex.Message}");
            // 发生错误时返回默认配置
            return new AppConfig();
        }
    }

    /// <summary>
    /// 保存配置到文件
    /// </summary>
    /// <param name="config">配置对象</param>
    /// <returns>是否保存成功</returns>
    public static bool SaveConfig(AppConfig config)
    {
        try
        {
            // 更新最后修改时间
            // config.LastUpdated = DateTime.Now;

            // 序列化对象为JSON字符串，格式化输出
            string jsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);

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

    /// <summary>
    /// 创建默认配置并保存
    /// </summary>
    /// <returns>默认配置对象</returns>
    private static AppConfig CreateDefaultConfig()
    {
        var defaultConfig = new AppConfig();
        SaveConfig(defaultConfig);
        Console.WriteLine($"已创建默认配置文件: {_configFilePath}");
        return defaultConfig;
    }
}