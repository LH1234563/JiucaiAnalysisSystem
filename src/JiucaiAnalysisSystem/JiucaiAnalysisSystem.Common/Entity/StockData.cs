namespace JiucaiAnalysisSystem.Common.Entity;

public class StockData
{
    /// <summary>
    /// 抓取日期（自己设定）
    /// </summary>
    public string radte_date { get; set; }

    /// <summary>
    /// f2	最新价 / 收盘价
    /// </summary>
    public decimal close_price { get; set; }

    /// <summary>
    /// f4	涨跌额
    /// </summary>
    public string change_amount { get; set; }

    /// <summary>
    /// f3	涨跌幅(%)
    /// </summary>
    public string change_rate { get; set; }

    /// <summary>
    /// f5	成交量（手）
    /// </summary>
    public string volume { get; set; }

    /// <summary>
    /// f6	成交额（元）
    /// </summary>
    public string turnover { get; set; }

    /// <summary>
    /// f7	振幅(%)
    /// </summary>
    public string amplitude { get; set; }

    /// <summary>
    /// f8	换手率(%)
    /// </summary>
    public string turnover_rate { get; set; }

    /// <summary>
    /// f9	市盈率（动态）
    /// </summary>
    public string pe_ratio { get; set; }

    /// <summary>
    /// f12	股票代码
    /// </summary>
    public string stock_code { get; set; }

    /// <summary>
    /// f14	股票名称
    /// </summary>
    public string stock_name { get; set; }

    /// <summary>
    /// f15	最高价
    /// </summary>
    public string high_price { get; set; }

    /// <summary>
    /// f16	最低价
    /// </summary>
    public string low_price { get; set; }

    /// <summary>
    /// f17	今开价
    /// </summary>
    public string open_price { get; set; }

    /// <summary>
    /// f23	市净率
    /// </summary>
    public string pb_ratio { get; set; }

    /// <summary>
    /// f20	总市值
    /// </summary>
    public string total_market_cap { get; set; }

    /// <summary>
    /// f21	流通市值
    /// </summary>
    public string circulating_market_cap { get; set; }
}