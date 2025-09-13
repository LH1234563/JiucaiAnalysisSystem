namespace JiucaiAnalysisSystem.Common.Utilities;

/// <summary>
/// 应用程序配置模型
/// </summary>
public class AppConfig
{
    /// <summary>
    /// 数据库服务器地址
    /// </summary>
    public string DbHost { get; set; } = "localhost";

    /// <summary>
    /// 数据库用户名
    /// </summary>
    public string DbUser { get; set; }= "root";

    /// <summary>
    /// 数据库密码
    /// </summary>
    public string DbPassword { get; set; }= "123456";

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DbName { get; set; }= "stockdb";

    /// <summary>
    /// 数据库端口
    /// </summary>
    public int DbPort { get; set; }

    /// <summary>
    /// 所有股票代码
    /// </summary>
    public List<string>? StockCodeAll { get; set; }

    /// <summary>
    /// 每日股票选择的日期
    /// </summary>
    public DateTime? SelectedDate { get; set; }
}