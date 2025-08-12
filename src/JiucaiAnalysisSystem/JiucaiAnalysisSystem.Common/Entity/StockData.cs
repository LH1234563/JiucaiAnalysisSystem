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

/// <summary>
/// 东方财富接口
/// </summary>
public class EastMoneyStock
{
    /// <summary>
    /// f2 最新价 / 收盘价
    /// </summary>
    public decimal close_price { get; set; }

    /// <summary>
    /// f3 涨跌幅(%)
    /// </summary>
    public decimal change_rate { get; set; }

    /// <summary>
    /// f4 涨跌额
    /// </summary>
    public decimal change_amount { get; set; }

    /// <summary>
    /// f5 总手
    /// </summary>
    public long volume { get; set; }

    /// <summary>
    /// f6 成交额
    /// </summary>
    public decimal turnover { get; set; }

    /// <summary>
    /// f7 振幅(%)
    /// </summary>
    public decimal amplitude { get; set; }

    /// <summary>
    /// f8 换手率(%)
    /// </summary>
    public decimal turnover_rate { get; set; }

    /// <summary>
    /// f9 市盈率（动态）
    /// </summary>
    public decimal pe_ratio { get; set; }

    /// <summary>
    /// f10 量比
    /// </summary>
    public decimal volume_ratio { get; set; }

    /// <summary>
    /// f11 5分钟涨跌幅
    /// </summary>
    public decimal change_rate_5min { get; set; }

    /// <summary>
    /// f12 股票代码
    /// </summary>
    public string stock_code { get; set; }

    /// <summary>
    /// f14 股票名称
    /// </summary>
    public string stock_name { get; set; }

    /// <summary>
    /// f15 最高价
    /// </summary>
    public decimal high_price { get; set; }

    /// <summary>
    /// f16 最低价
    /// </summary>
    public decimal low_price { get; set; }

    /// <summary>
    /// f17 开盘价
    /// </summary>
    public decimal open_price { get; set; }

    /// <summary>
    /// f18 昨收
    /// </summary>
    public decimal prev_close { get; set; }

    /// <summary>
    /// f20 总市值
    /// </summary>
    public decimal total_market_cap { get; set; }

    /// <summary>
    /// f21 流通市值
    /// </summary>
    public decimal circulating_market_cap { get; set; }

    /// <summary>
    /// f22 涨速
    /// </summary>
    public decimal rise_speed { get; set; }

    /// <summary>
    /// f23 市净率
    /// </summary>
    public decimal pb_ratio { get; set; }

    /// <summary>
    /// f24 60日涨跌幅
    /// </summary>
    public decimal change_rate_60d { get; set; }

    /// <summary>
    /// f25 年初至今涨跌幅
    /// </summary>
    public decimal change_rate_ytd { get; set; }

    /// <summary>
    /// f26 上市日期
    /// </summary>
    public DateTime listing_date { get; set; }

    /// <summary>
    /// f30 现手
    /// </summary>
    public long current_volume { get; set; }

    /// <summary>
    /// f33 委比
    /// </summary>
    public decimal order_ratio { get; set; }

    /// <summary>
    /// f34 外盘
    /// </summary>
    public long out_volume { get; set; }

    /// <summary>
    /// f35 内盘
    /// </summary>
    public long in_volume { get; set; }

    /// <summary>
    /// f36 人均持股数
    /// </summary>
    public decimal avg_hold_shares { get; set; }

    /// <summary>
    /// f37 净资产收益率(加权)
    /// </summary>
    public decimal roe_weighted { get; set; }

    /// <summary>
    /// f38 总股本
    /// </summary>
    public decimal total_shares { get; set; }

    /// <summary>
    /// f39 流通股
    /// </summary>
    public decimal circulating_shares { get; set; }

    /// <summary>
    /// f40 营业收入
    /// </summary>
    public decimal revenue { get; set; }

    /// <summary>
    /// f41 营业收入同比
    /// </summary>
    public decimal revenue_yoy { get; set; }

    /// <summary>
    /// f42 营业利润
    /// </summary>
    public decimal operating_profit { get; set; }

    /// <summary>
    /// f43 投资收益
    /// </summary>
    public decimal investment_income { get; set; }

    /// <summary>
    /// f44 利润总额
    /// </summary>
    public decimal total_profit { get; set; }

    /// <summary>
    /// f45 净利润
    /// </summary>
    public decimal net_profit { get; set; }

    /// <summary>
    /// f46 净利润同比
    /// </summary>
    public decimal net_profit_yoy { get; set; }

    /// <summary>
    /// f47 未分配利润
    /// </summary>
    public decimal undistributed_profit { get; set; }

    /// <summary>
    /// f48 每股未分配利润
    /// </summary>
    public decimal undistributed_profit_per_share { get; set; }

    /// <summary>
    /// f49 毛利率
    /// </summary>
    public decimal gross_profit_margin { get; set; }

    /// <summary>
    /// f50 总资产
    /// </summary>
    public decimal total_assets { get; set; }

    /// <summary>
    /// f51 流动资产
    /// </summary>
    public decimal current_assets { get; set; }

    /// <summary>
    /// f52 固定资产
    /// </summary>
    public decimal fixed_assets { get; set; }

    /// <summary>
    /// f53 无形资产
    /// </summary>
    public decimal intangible_assets { get; set; }

    /// <summary>
    /// f54 总负债
    /// </summary>
    public decimal total_liabilities { get; set; }

    /// <summary>
    /// f55 流动负债
    /// </summary>
    public decimal current_liabilities { get; set; }

    /// <summary>
    /// f56 长期负债
    /// </summary>
    public decimal long_term_liabilities { get; set; }

    /// <summary>
    /// f57 资产负债比率
    /// </summary>
    public decimal debt_ratio { get; set; }

    /// <summary>
    /// f62 主力净流入
    /// </summary>
    public decimal main_net_inflow { get; set; }

    /// <summary>
    /// f63 集合竞价
    /// </summary>
    public decimal call_auction { get; set; }

    /// <summary>
    /// f64 超大单流入
    /// </summary>
    public decimal ultra_large_inflow { get; set; }

    /// <summary>
    /// f65 超大单流出
    /// </summary>
    public decimal ultra_large_outflow { get; set; }

    /// <summary>
    /// f66 超大单净额
    /// </summary>
    public decimal ultra_large_net { get; set; }

    /// <summary>
    /// f69 超大单净占比
    /// </summary>
    public decimal ultra_large_net_ratio { get; set; }

    /// <summary>
    /// f70 大单流入
    /// </summary>
    public decimal large_inflow { get; set; }

    /// <summary>
    /// f71 大单流出
    /// </summary>
    public decimal large_outflow { get; set; }

    /// <summary>
    /// f72 大单净额
    /// </summary>
    public decimal large_net { get; set; }

    /// <summary>
    /// f75 大单净占比
    /// </summary>
    public decimal large_net_ratio { get; set; }

    /// <summary>
    /// f76 中单流入
    /// </summary>
    public decimal medium_inflow { get; set; }

    /// <summary>
    /// f77 中单流出
    /// </summary>
    public decimal medium_outflow { get; set; }

    /// <summary>
    /// f78 中单净额
    /// </summary>
    public decimal medium_net { get; set; }

    /// <summary>
    /// f81 中单净占比
    /// </summary>
    public decimal medium_net_ratio { get; set; }

    /// <summary>
    /// f82 小单流入
    /// </summary>
    public decimal small_inflow { get; set; }

    /// <summary>
    /// f83 小单流出
    /// </summary>
    public decimal small_outflow { get; set; }

    /// <summary>
    /// f84 小单净额
    /// </summary>
    public decimal small_net { get; set; }

    /// <summary>
    /// f87 小单净占比
    /// </summary>
    public decimal small_net_ratio { get; set; }

    /// <summary>
    /// f88 当日DDX
    /// </summary>
    public decimal ddx_today { get; set; }

    /// <summary>
    /// f89 当日DDY
    /// </summary>
    public decimal ddy_today { get; set; }

    /// <summary>
    /// f90 当日DDZ
    /// </summary>
    public decimal ddz_today { get; set; }

    /// <summary>
    /// f91 5日DDX
    /// </summary>
    public decimal ddx_5d { get; set; }

    /// <summary>
    /// f92 5日DDY
    /// </summary>
    public decimal ddy_5d { get; set; }

    /// <summary>
    /// f94 10日DDX
    /// </summary>
    public decimal ddx_10d { get; set; }

    /// <summary>
    /// f95 10日DDY
    /// </summary>
    public decimal ddy_10d { get; set; }

    /// <summary>
    /// f97 DDX飘红天数(连续)
    /// </summary>
    public int ddx_red_days { get; set; }

    /// <summary>
    /// f98 DDX飘红天数(5日)
    /// </summary>
    public int ddx_red_days_5d { get; set; }

    /// <summary>
    /// f99 DDX飘红天数(10日)
    /// </summary>
    public int ddx_red_days_10d { get; set; }

    /// <summary>
    /// f100 行业
    /// </summary>
    public string industry { get; set; }

    /// <summary>
    /// f101 板块领涨股
    /// </summary>
    public string sector_leader_stock { get; set; }

    /// <summary>
    /// f102 地区板块
    /// </summary>
    public string region_sector { get; set; }

    /// <summary>
    /// f114 市盈率（静）
    /// </summary>
    public decimal pe_static { get; set; }

    /// <summary>
    /// f115 市盈率（TTM）
    /// </summary>
    public decimal pe_ttm { get; set; }

    /// <summary>
    /// f221 更新日期
    /// </summary>
    public DateTime update_date { get; set; }
}

// public class EastMoneyStock
// {
//     /// <summary>
//     /// f2 最新价 / 收盘价
//     /// </summary>
//     public decimal close_price { get; set; }
//
//     /// <summary>
//     /// f3 涨跌幅(%)
//     /// </summary>
//     public decimal change_rate { get; set; }
//
//     /// <summary>
//     /// f4 涨跌额
//     /// </summary>
//     public decimal change_amount { get; set; }
//
//     /// <summary>
//     /// f5 总手
//     /// </summary>
//     public long volume { get; set; }
//
//     /// <summary>
//     /// f6 成交额
//     /// </summary>
//     public decimal turnover { get; set; }
//
//     /// <summary>
//     /// f7 振幅(%)
//     /// </summary>
//     public decimal amplitude { get; set; }
//
//     /// <summary>
//     /// f8 换手率(%)
//     /// </summary>
//     public decimal turnover_rate { get; set; }
//
//     /// <summary>
//     /// f9 市盈率（动态）
//     /// </summary>
//     public decimal pe_ratio { get; set; }
//
//     /// <summary>
//     /// f10 量比
//     /// </summary>
//     public decimal volume_ratio { get; set; }
//
//     /// <summary>
//     /// f11 5分钟涨跌幅
//     /// </summary>
//     public decimal change_rate_5min { get; set; }
//
//     /// <summary>
//     /// f12 股票代码
//     /// </summary>
//     public string stock_code { get; set; }
//
//     /// <summary>
//     /// f13 市场
//     /// </summary>
//     public int market { get; set; }
//
//     /// <summary>
//     /// f14 股票名称
//     /// </summary>
//     public string stock_name { get; set; }
//
//     /// <summary>
//     /// f15 最高价
//     /// </summary>
//     public decimal high_price { get; set; }
//
//     /// <summary>
//     /// f16 最低价
//     /// </summary>
//     public decimal low_price { get; set; }
//
//     /// <summary>
//     /// f17 开盘价
//     /// </summary>
//     public decimal open_price { get; set; }
//
//     /// <summary>
//     /// f18 昨收
//     /// </summary>
//     public decimal prev_close { get; set; }
//
//     /// <summary>
//     /// f20 总市值
//     /// </summary>
//     public decimal total_market_cap { get; set; }
//
//     /// <summary>
//     /// f21 流通市值
//     /// </summary>
//     public decimal circulating_market_cap { get; set; }
//
//     /// <summary>
//     /// f22 涨速
//     /// </summary>
//     public decimal rise_speed { get; set; }
//
//     /// <summary>
//     /// f23 市净率
//     /// </summary>
//     public decimal pb_ratio { get; set; }
//
//     /// <summary>
//     /// f24 60日涨跌幅
//     /// </summary>
//     public decimal change_rate_60d { get; set; }
//
//     /// <summary>
//     /// f25 年初至今涨跌幅
//     /// </summary>
//     public decimal change_rate_ytd { get; set; }
//
//     /// <summary>
//     /// f26 上市日期
//     /// </summary>
//     public DateTime listing_date { get; set; }
//
//     /// <summary>
//     /// f28 昨日结算价
//     /// </summary>
//     public decimal prev_settlement { get; set; }
//
//     /// <summary>
//     /// f30 现手
//     /// </summary>
//     public long current_volume { get; set; }
//
//     /// <summary>
//     /// f31 买入价
//     /// </summary>
//     public decimal bid_price { get; set; }
//
//     /// <summary>
//     /// f32 卖出价
//     /// </summary>
//     public decimal ask_price { get; set; }
//
//     /// <summary>
//     /// f33 委比
//     /// </summary>
//     public decimal order_ratio { get; set; }
//
//     /// <summary>
//     /// f34 外盘
//     /// </summary>
//     public long out_volume { get; set; }
//
//     /// <summary>
//     /// f35 内盘
//     /// </summary>
//     public long in_volume { get; set; }
//
//     /// <summary>
//     /// f36 人均持股数
//     /// </summary>
//     public decimal avg_hold_shares { get; set; }
//
//     /// <summary>
//     /// f37 净资产收益率(加权)
//     /// </summary>
//     public decimal roe_weighted { get; set; }
//
//     /// <summary>
//     /// f38 总股本
//     /// </summary>
//     public decimal total_shares { get; set; }
//
//     /// <summary>
//     /// f39 流通股
//     /// </summary>
//     public decimal circulating_shares { get; set; }
//
//     /// <summary>
//     /// f40 营业收入
//     /// </summary>
//     public decimal revenue { get; set; }
//
//     /// <summary>
//     /// f41 营业收入同比
//     /// </summary>
//     public decimal revenue_yoy { get; set; }
//
//     /// <summary>
//     /// f42 营业利润
//     /// </summary>
//     public decimal operating_profit { get; set; }
//
//     /// <summary>
//     /// f43 投资收益
//     /// </summary>
//     public decimal investment_income { get; set; }
//
//     /// <summary>
//     /// f44 利润总额
//     /// </summary>
//     public decimal total_profit { get; set; }
//
//     /// <summary>
//     /// f45 净利润
//     /// </summary>
//     public decimal net_profit { get; set; }
//
//     /// <summary>
//     /// f46 净利润同比
//     /// </summary>
//     public decimal net_profit_yoy { get; set; }
//
//     /// <summary>
//     /// f47 未分配利润
//     /// </summary>
//     public decimal undistributed_profit { get; set; }
//
//     /// <summary>
//     /// f48 每股未分配利润
//     /// </summary>
//     public decimal undistributed_profit_per_share { get; set; }
//
//     /// <summary>
//     /// f49 毛利率
//     /// </summary>
//     public decimal gross_profit_margin { get; set; }
//
//     /// <summary>
//     /// f50 总资产
//     /// </summary>
//     public decimal total_assets { get; set; }
//
//     /// <summary>
//     /// f51 流动资产
//     /// </summary>
//     public decimal current_assets { get; set; }
//
//     /// <summary>
//     /// f52 固定资产
//     /// </summary>
//     public decimal fixed_assets { get; set; }
//
//     /// <summary>
//     /// f53 无形资产
//     /// </summary>
//     public decimal intangible_assets { get; set; }
//
//     /// <summary>
//     /// f54 总负债
//     /// </summary>
//     public decimal total_liabilities { get; set; }
//
//     /// <summary>
//     /// f55 流动负债
//     /// </summary>
//     public decimal current_liabilities { get; set; }
//
//     /// <summary>
//     /// f56 长期负债
//     /// </summary>
//     public decimal long_term_liabilities { get; set; }
//
//     /// <summary>
//     /// f57 资产负债比率
//     /// </summary>
//     public decimal debt_ratio { get; set; }
//
//     /// <summary>
//     /// f58 股东权益
//     /// </summary>
//     public decimal equity { get; set; }
//
//     /// <summary>
//     /// f59 股东权益比
//     /// </summary>
//     public decimal equity_ratio { get; set; }
//
//     /// <summary>
//     /// f60 公积金
//     /// </summary>
//     public decimal capital_reserve { get; set; }
//
//     /// <summary>
//     /// f61 每股公积金
//     /// </summary>
//     public decimal capital_reserve_per_share { get; set; }
//
//     /// <summary>
//     /// f62 主力净流入
//     /// </summary>
//     public decimal main_net_inflow { get; set; }
//
//     /// <summary>
//     /// f63 集合竞价
//     /// </summary>
//     public decimal call_auction { get; set; }
//
//     /// <summary>
//     /// f64 超大单流入
//     /// </summary>
//     public decimal ultra_large_inflow { get; set; }
//
//     /// <summary>
//     /// f65 超大单流出
//     /// </summary>
//     public decimal ultra_large_outflow { get; set; }
//
//     /// <summary>
//     /// f66 超大单净额
//     /// </summary>
//     public decimal ultra_large_net { get; set; }
//
//     /// <summary>
//     /// f69 超大单净占比
//     /// </summary>
//     public decimal ultra_large_net_ratio { get; set; }
//
//     /// <summary>
//     /// f70 大单流入
//     /// </summary>
//     public decimal large_inflow { get; set; }
//
//     /// <summary>
//     /// f71 大单流出
//     /// </summary>
//     public decimal large_outflow { get; set; }
//
//     /// <summary>
//     /// f72 大单净额
//     /// </summary>
//     public decimal large_net { get; set; }
//
//     /// <summary>
//     /// f75 大单净占比
//     /// </summary>
//     public decimal large_net_ratio { get; set; }
//
//     /// <summary>
//     /// f76 中单流入
//     /// </summary>
//     public decimal medium_inflow { get; set; }
//
//     /// <summary>
//     /// f77 中单流出
//     /// </summary>
//     public decimal medium_outflow { get; set; }
//
//     /// <summary>
//     /// f78 中单净额
//     /// </summary>
//     public decimal medium_net { get; set; }
//
//     /// <summary>
//     /// f81 中单净占比
//     /// </summary>
//     public decimal medium_net_ratio { get; set; }
//
//     /// <summary>
//     /// f82 小单流入
//     /// </summary>
//     public decimal small_inflow { get; set; }
//
//     /// <summary>
//     /// f83 小单流出
//     /// </summary>
//     public decimal small_outflow { get; set; }
//
//     /// <summary>
//     /// f84 小单净额
//     /// </summary>
//     public decimal small_net { get; set; }
//
//     /// <summary>
//     /// f87 小单净占比
//     /// </summary>
//     public decimal small_net_ratio { get; set; }
//
//     /// <summary>
//     /// f88 当日DDX
//     /// </summary>
//     public decimal ddx_today { get; set; }
//
//     /// <summary>
//     /// f89 当日DDY
//     /// </summary>
//     public decimal ddy_today { get; set; }
//
//     /// <summary>
//     /// f90 当日DDZ
//     /// </summary>
//     public decimal ddz_today { get; set; }
//
//     /// <summary>
//     /// f91 5日DDX
//     /// </summary>
//     public decimal ddx_5d { get; set; }
//
//     /// <summary>
//     /// f92 5日DDY
//     /// </summary>
//     public decimal ddy_5d { get; set; }
//
//     /// <summary>
//     /// f94 10日DDX
//     /// </summary>
//     public decimal ddx_10d { get; set; }
//
//     /// <summary>
//     /// f95 10日DDY
//     /// </summary>
//     public decimal ddy_10d { get; set; }
//
//     /// <summary>
//     /// f97 DDX飘红天数(连续)
//     /// </summary>
//     public int ddx_red_days { get; set; }
//
//     /// <summary>
//     /// f98 DDX飘红天数(5日)
//     /// </summary>
//     public int ddx_red_days_5d { get; set; }
//
//     /// <summary>
//     /// f99 DDX飘红天数(10日)
//     /// </summary>
//     public int ddx_red_days_10d { get; set; }
//
//     /// <summary>
//     /// f100 行业
//     /// </summary>
//     public string industry { get; set; }
//
//     /// <summary>
//     /// f101 板块领涨股
//     /// </summary>
//     public string sector_leader_stock { get; set; }
//
//     /// <summary>
//     /// f102 地区板块
//     /// </summary>
//     public string region_sector { get; set; }
//
//     /// <summary>
//     /// f103 备注
//     /// </summary>
//     public string remark { get; set; }
//
//     /// <summary>
//     /// f104 上涨家数
//     /// </summary>
//     public int rise_count { get; set; }
//
//     /// <summary>
//     /// f105 下跌家数
//     /// </summary>
//     public int fall_count { get; set; }
//
//     /// <summary>
//     /// f106 平家家数
//     /// </summary>
//     public int flat_count { get; set; }
//
//     /// <summary>
//     /// f112 每股收益
//     /// </summary>
//     public decimal eps { get; set; }
//
//     /// <summary>
//     /// f113 每股净资产
//     /// </summary>
//     public decimal nav_per_share { get; set; }
//
//     /// <summary>
//     /// f114 市盈率（静）
//     /// </summary>
//     public decimal pe_static { get; set; }
//
//     /// <summary>
//     /// f115 市盈率（TTM）
//     /// </summary>
//     public decimal pe_ttm { get; set; }
//
//     /// <summary>
//     /// f124 当前交易时间
//     /// </summary>
//     public string trade_time { get; set; }
//
//     /// <summary>
//     /// f128 板块领涨股
//     /// </summary>
//     public string sector_leader { get; set; }
//
//     /// <summary>
//     /// f129 净利润
//     /// </summary>
//     public decimal net_profit2 { get; set; }
//
//     /// <summary>
//     /// f130 市销率TTM
//     /// </summary>
//     public decimal ps_ttm { get; set; }
//
//     /// <summary>
//     /// f131 市现率TTM
//     /// </summary>
//     public decimal pc_ttm { get; set; }
//
//     /// <summary>
//     /// f132 总营业收入TTM
//     /// </summary>
//     public decimal revenue_ttm { get; set; }
//
//     /// <summary>
//     /// f133 股息率
//     /// </summary>
//     public decimal dividend_yield { get; set; }
//
//     /// <summary>
//     /// f134 行业板块的成分股数
//     /// </summary>
//     public int industry_stock_count { get; set; }
//
//     /// <summary>
//     /// f135 净资产
//     /// </summary>
//     public decimal net_assets { get; set; }
//
//     /// <summary>
//     /// f138 净利润TTM
//     /// </summary>
//     public decimal net_profit_ttm { get; set; }
//
//     /// <summary>
//     /// f221 更新日期
//     /// </summary>
//     public DateTime update_date { get; set; }
// }