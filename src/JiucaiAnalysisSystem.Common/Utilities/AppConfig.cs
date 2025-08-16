using Newtonsoft.Json;

namespace JiucaiAnalysisSystem.Common.Utilities;

/// <summary>
/// 应用程序配置模型
/// </summary>
public class AppConfig
{
    /// <summary>
    /// 数据库服务器地址
    /// </summary>
    public string DbHost { get; set; } 

    /// <summary>
    /// 数据库用户名
    /// </summary>
    public string DbUser { get; set; }

    /// <summary>
    /// 数据库密码
    /// </summary>
    public string DbPassword { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DbName { get; set; } 

    /// <summary>
    /// 数据库端口
    /// </summary>
    public int DbPort { get; set; }
    /// <summary>
    /// 所有股票代码
    /// </summary>
    public List<string> StockCodeAll { get; set; }
}