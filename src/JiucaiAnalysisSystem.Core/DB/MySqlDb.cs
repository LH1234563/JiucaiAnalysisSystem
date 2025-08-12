using JiucaiAnalysisSystem.Common.Entity;
using JiucaiAnalysisSystem.Common.Utilities;
using MySqlConnector;

namespace JiucaiAnalysisSystem.Core.DB;

public class MySqlDb
{
    /// <summary>
    /// 检查数据库是否存在
    /// </summary>
    /// <returns>如果存在返回true，否则返回false</returns>
    public bool CheckDatabaseExists()
    {
        try
        {
            AppConfig config = ConfigManager.LoadConfig();
            string checkQuery =
                $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{config.DbName}'";
            return DbCommand.ExecuteCommand(checkQuery);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"检查数据库存在性时出错: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// 创建数据库
    /// </summary>
    /// <returns>如果创建成功返回true，否则返回false</returns>
    public bool CreateDatabase()
    {
        try
        {
            AppConfig config = ConfigManager.LoadConfig();
            // 创建数据库的SQL命令
            string createDbBaseQuery =
                $"CREATE DATABASE IF NOT EXISTS {config.DbName} CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci";
            DbCommand.ExecuteNonQueryCommand(createDbBaseQuery);
            var createTableQuery = $"CREATE TABLE `{config.DbName}`.`stock_inventory` (" +
                                   $"`id` BIGINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT '自增主键'," +
                                   $"`close_price` DECIMAL(18,4) DEFAULT NULL COMMENT '最新价 / 收盘价'," +
                                   $"`change_rate` DECIMAL(10,4) DEFAULT NULL COMMENT '涨跌幅(%)'," +
                                   $"`change_amount` DECIMAL(18,4) DEFAULT NULL COMMENT '涨跌额'," +
                                   $"`volume` BIGINT DEFAULT NULL COMMENT '总手'," +
                                   $"`turnover` DECIMAL(20,4) DEFAULT NULL COMMENT '成交额'," +
                                   $"`amplitude` DECIMAL(10,4) DEFAULT NULL COMMENT '振幅(%)'," +
                                   $"`turnover_rate` DECIMAL(10,4) DEFAULT NULL COMMENT '换手率(%)'," +
                                   $"`pe_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '市盈率（动态）'," +
                                   $"`volume_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '量比'," +
                                   $"`change_rate_5min` DECIMAL(10,4) DEFAULT NULL COMMENT '5分钟涨跌幅'," +
                                   $"`stock_code` VARCHAR(20) NOT NULL COMMENT '股票代码'," +
                                   $"`stock_name` VARCHAR(50) DEFAULT NULL COMMENT '股票名称'," +
                                   $"`high_price` DECIMAL(18,4) DEFAULT NULL COMMENT '最高价'," +
                                   $"`low_price` DECIMAL(18,4) DEFAULT NULL COMMENT '最低价'," +
                                   $"`open_price` DECIMAL(18,4) DEFAULT NULL COMMENT '开盘价'," +
                                   $"`prev_close` DECIMAL(18,4) DEFAULT NULL COMMENT '昨收'," +
                                   $"`total_market_cap` DECIMAL(20,4) DEFAULT NULL COMMENT '总市值'," +
                                   $"`circulating_market_cap` DECIMAL(20,4) DEFAULT NULL COMMENT '流通市值'," +
                                   $"`rise_speed` DECIMAL(10,4) DEFAULT NULL COMMENT '涨速'," +
                                   $"`pb_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '市净率'," +
                                   $"`change_rate_60d` DECIMAL(10,4) DEFAULT NULL COMMENT '60日涨跌幅'," +
                                   $"`change_rate_ytd` DECIMAL(10,4) DEFAULT NULL COMMENT '年初至今涨跌幅'," +
                                   $"`listing_date` DATE DEFAULT NULL COMMENT '上市日期'," +
                                   $"`current_volume` BIGINT DEFAULT NULL COMMENT '现手'," +
                                   $"`order_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '委比'," +
                                   $"`out_volume` BIGINT DEFAULT NULL COMMENT '外盘'," +
                                   $"`in_volume` BIGINT DEFAULT NULL COMMENT '内盘'," +
                                   $"`avg_hold_shares` DECIMAL(18,4) DEFAULT NULL COMMENT '人均持股数'," +
                                   $"`roe_weighted` DECIMAL(10,4) DEFAULT NULL COMMENT '净资产收益率(加权)'," +
                                   $"`total_shares` DECIMAL(20,4) DEFAULT NULL COMMENT '总股本'," +
                                   $"`circulating_shares` DECIMAL(20,4) DEFAULT NULL COMMENT '流通股'," +
                                   $"`revenue` DECIMAL(20,4) DEFAULT NULL COMMENT '营业收入'," +
                                   $"`revenue_yoy` DECIMAL(10,4) DEFAULT NULL COMMENT '营业收入同比'," +
                                   $"`operating_profit` DECIMAL(20,4) DEFAULT NULL COMMENT '营业利润'," +
                                   $"`investment_income` DECIMAL(20,4) DEFAULT NULL COMMENT '投资收益'," +
                                   $"`total_profit` DECIMAL(20,4) DEFAULT NULL COMMENT '利润总额'," +
                                   $"`net_profit` DECIMAL(20,4) DEFAULT NULL COMMENT '净利润'," +
                                   $"`net_profit_yoy` DECIMAL(10,4) DEFAULT NULL COMMENT '净利润同比'," +
                                   $"`undistributed_profit` DECIMAL(20,4) DEFAULT NULL COMMENT '未分配利润'," +
                                   $"`undistributed_profit_per_share` DECIMAL(18,4) DEFAULT NULL COMMENT '每股未分配利润'," +
                                   $"`gross_profit_margin` DECIMAL(10,4) DEFAULT NULL COMMENT '毛利率'," +
                                   $"`total_assets` DECIMAL(20,4) DEFAULT NULL COMMENT '总资产'," +
                                   $"`current_assets` DECIMAL(20,4) DEFAULT NULL COMMENT '流动资产'," +
                                   $"`fixed_assets` DECIMAL(20,4) DEFAULT NULL COMMENT '固定资产'," +
                                   $"`intangible_assets` DECIMAL(20,4) DEFAULT NULL COMMENT '无形资产'," +
                                   $"`total_liabilities` DECIMAL(20,4) DEFAULT NULL COMMENT '总负债'," +
                                   $"`current_liabilities` DECIMAL(20,4) DEFAULT NULL COMMENT '流动负债'," +
                                   $"`long_term_liabilities` DECIMAL(20,4) DEFAULT NULL COMMENT '长期负债'," +
                                   $"`debt_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '资产负债比率'," +
                                   $"`main_net_inflow` DECIMAL(20,4) DEFAULT NULL COMMENT '主力净流入'," +
                                   $"`call_auction` DECIMAL(20,4) DEFAULT NULL COMMENT '集合竞价'," +
                                   $"`ultra_large_inflow` DECIMAL(20,4) DEFAULT NULL COMMENT '超大单流入'," +
                                   $"`ultra_large_outflow` DECIMAL(20,4) DEFAULT NULL COMMENT '超大单流出'," +
                                   $"`ultra_large_net` DECIMAL(20,4) DEFAULT NULL COMMENT '超大单净额'," +
                                   $"`ultra_large_net_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '超大单净占比'," +
                                   $"`large_inflow` DECIMAL(20,4) DEFAULT NULL COMMENT '大单流入'," +
                                   $"`large_outflow` DECIMAL(20,4) DEFAULT NULL COMMENT '大单流出'," +
                                   $"`large_net` DECIMAL(20,4) DEFAULT NULL COMMENT '大单净额'," +
                                   $"`large_net_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '大单净占比'," +
                                   $"`medium_inflow` DECIMAL(20,4) DEFAULT NULL COMMENT '中单流入'," +
                                   $"`medium_outflow` DECIMAL(20,4) DEFAULT NULL COMMENT '中单流出'," +
                                   $"`medium_net` DECIMAL(20,4) DEFAULT NULL COMMENT '中单净额'" +
                                   $",`medium_net_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '中单净占比'," +
                                   $"`small_inflow` DECIMAL(20,4) DEFAULT NULL COMMENT '小单流入'," +
                                   $"`small_outflow` DECIMAL(20,4) DEFAULT NULL COMMENT '小单流出'," +
                                   $"`small_net` DECIMAL(20,4) DEFAULT NULL COMMENT '小单净额'," +
                                   $"`small_net_ratio` DECIMAL(10,4) DEFAULT NULL COMMENT '小单净占比'," +
                                   $"`ddx_today` DECIMAL(10,4) DEFAULT NULL COMMENT '当日DDX'," +
                                   $"`ddy_today` DECIMAL(10,4) DEFAULT NULL COMMENT '当日DDY'," +
                                   $"`ddz_today` DECIMAL(10,4) DEFAULT NULL COMMENT '当日DDZ'," +
                                   $"`ddx_5d` DECIMAL(10,4) DEFAULT NULL COMMENT '5日DDX'," +
                                   $"`ddy_5d` DECIMAL(10,4) DEFAULT NULL COMMENT '5日DDY'," +
                                   $"`ddx_10d` DECIMAL(10,4) DEFAULT NULL COMMENT '10日DDX'," +
                                   $"`ddy_10d` DECIMAL(10,4) DEFAULT NULL COMMENT '10日DDY'," +
                                   $"`ddx_red_days` INT DEFAULT NULL COMMENT 'DDX飘红天数(连续)'," +
                                   $"`ddx_red_days_5d` INT DEFAULT NULL COMMENT 'DDX飘红天数(5日)'," +
                                   $"`ddx_red_days_10d` INT DEFAULT NULL COMMENT 'DDX飘红天数(10日)'," +
                                   $"`industry` VARCHAR(100) DEFAULT NULL COMMENT '行业'," +
                                   $"`sector_leader_stock` VARCHAR(100) DEFAULT NULL COMMENT '板块领涨股'," +
                                   $"`region_sector` VARCHAR(100) DEFAULT NULL COMMENT '地区板块'," +
                                   $"`pe_static` DECIMAL(10,4) DEFAULT NULL COMMENT '市盈率（静）'," +
                                   $"`pe_ttm` DECIMAL(10,4) DEFAULT NULL COMMENT '市盈率（TTM）'," +
                                   $"`update_date` DATE NOT NULL COMMENT '更新日期'" +
                                   $") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='股票历史数据'";
            DbCommand.ExecuteNonQueryCommand(createTableQuery);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"创建数据库时出错: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// 检查并创建数据库（如果不存在）
    /// </summary>
    public void CheckAndCreateDatabase()
    {
        AppConfig config = ConfigManager.LoadConfig();
        if (CheckDatabaseExists())
        {
            Console.WriteLine($"数据库 '{config.DbName}' 已存在。");
            return;
        }

        Console.WriteLine($"数据库 '{config.DbName}' 不存在，正在创建...");
        if (CreateDatabase())
        {
            Console.WriteLine($"数据库 '{config.DbName}' 创建成功...");
        }
    }

    public async Task SaveToMySql(List<EastMoneyStock> list)
    {
        try
        {
            AppConfig config = ConfigManager.LoadConfig();
            string connStr = DbCommand.GetConnectionString();
            using var conn = new MySqlConnection(connStr);
            await conn.OpenAsync();
            string sql = @"
        INSERT INTO stock_history (
            close_price, change_rate, change_amount, volume, turnover, amplitude, turnover_rate, 
            pe_ratio, volume_ratio, change_rate_5min, stock_code, stock_name, high_price, low_price, 
            open_price, prev_close, total_market_cap, circulating_market_cap, rise_speed, pb_ratio, 
            change_rate_60d, change_rate_ytd, listing_date, current_volume, order_ratio, out_volume, 
            in_volume, avg_hold_shares, roe_weighted, total_shares, circulating_shares, revenue, 
            revenue_yoy, operating_profit, investment_income, total_profit, net_profit, net_profit_yoy, 
            undistributed_profit, undistributed_profit_per_share, gross_profit_margin, total_assets, 
            current_assets, fixed_assets, intangible_assets, total_liabilities, current_liabilities, 
            long_term_liabilities, debt_ratio, main_net_inflow, call_auction, ultra_large_inflow, 
            ultra_large_outflow, ultra_large_net, ultra_large_net_ratio, large_inflow, large_outflow, 
            large_net, large_net_ratio, medium_inflow, medium_outflow, medium_net, medium_net_ratio, 
            small_inflow, small_outflow, small_net, small_net_ratio, ddx_today, ddy_today, ddz_today, 
            ddx_5d, ddy_5d, ddx_10d, ddy_10d, ddx_red_days, ddx_red_days_5d, ddx_red_days_10d, industry, 
            sector_leader_stock, region_sector, pe_static, pe_ttm, update_date
        ) VALUES (
            @close_price, @change_rate, @change_amount, @volume, @turnover, @amplitude, @turnover_rate, 
            @pe_ratio, @volume_ratio, @change_rate_5min, @stock_code, @stock_name, @high_price, @low_price, 
            @open_price, @prev_close, @total_market_cap, @circulating_market_cap, @rise_speed, @pb_ratio, 
            @change_rate_60d, @change_rate_ytd, @listing_date, @current_volume, @order_ratio, @out_volume, 
            @in_volume, @avg_hold_shares, @roe_weighted, @total_shares, @circulating_shares, @revenue, 
            @revenue_yoy, @operating_profit, @investment_income, @total_profit, @net_profit, @net_profit_yoy, 
            @undistributed_profit, @undistributed_profit_per_share, @gross_profit_margin, @total_assets, 
            @current_assets, @fixed_assets, @intangible_assets, @total_liabilities, @current_liabilities, 
            @long_term_liabilities, @debt_ratio, @main_net_inflow, @call_auction, @ultra_large_inflow, 
            @ultra_large_outflow, @ultra_large_net, @ultra_large_net_ratio, @large_inflow, @large_outflow, 
            @large_net, @large_net_ratio, @medium_inflow, @medium_outflow, @medium_net, @medium_net_ratio, 
            @small_inflow, @small_outflow, @small_net, @small_net_ratio, @ddx_today, @ddy_today, @ddz_today, 
            @ddx_5d, @ddy_5d, @ddx_10d, @ddy_10d, @ddx_red_days, @ddx_red_days_5d, @ddx_red_days_10d, @industry, 
            @sector_leader_stock, @region_sector, @pe_static, @pe_ttm, @update_date
        );";
            using var cmd = new MySqlCommand(sql, conn);
            foreach (var stock in list)
            {
                cmd.Parameters.AddWithValue("@close_price", stock.close_price);
                cmd.Parameters.AddWithValue("@change_rate", stock.change_rate);
                cmd.Parameters.AddWithValue("@change_amount", stock.change_amount);
                cmd.Parameters.AddWithValue("@volume", stock.volume);
                cmd.Parameters.AddWithValue("@turnover", stock.turnover);
                cmd.Parameters.AddWithValue("@amplitude", stock.amplitude);
                cmd.Parameters.AddWithValue("@turnover_rate", stock.turnover_rate);
                cmd.Parameters.AddWithValue("@pe_ratio", stock.pe_ratio);
                cmd.Parameters.AddWithValue("@volume_ratio", stock.volume_ratio);
                cmd.Parameters.AddWithValue("@change_rate_5min", stock.change_rate_5min);
                cmd.Parameters.AddWithValue("@stock_code", stock.stock_code);
                cmd.Parameters.AddWithValue("@stock_name", stock.stock_name);
                cmd.Parameters.AddWithValue("@high_price", stock.high_price);
                cmd.Parameters.AddWithValue("@low_price", stock.low_price);
                cmd.Parameters.AddWithValue("@open_price", stock.open_price);
                cmd.Parameters.AddWithValue("@prev_close", stock.prev_close);
                cmd.Parameters.AddWithValue("@total_market_cap", stock.total_market_cap);
                cmd.Parameters.AddWithValue("@circulating_market_cap", stock.circulating_market_cap);
                cmd.Parameters.AddWithValue("@rise_speed", stock.rise_speed);
                cmd.Parameters.AddWithValue("@pb_ratio", stock.pb_ratio);
                cmd.Parameters.AddWithValue("@change_rate_60d", stock.change_rate_60d);
                cmd.Parameters.AddWithValue("@change_rate_ytd", stock.change_rate_ytd);
                cmd.Parameters.AddWithValue("@listing_date", stock.listing_date);
                cmd.Parameters.AddWithValue("@current_volume", stock.current_volume);
                cmd.Parameters.AddWithValue("@order_ratio", stock.order_ratio);
                cmd.Parameters.AddWithValue("@out_volume", stock.out_volume);
                cmd.Parameters.AddWithValue("@in_volume", stock.in_volume);
                cmd.Parameters.AddWithValue("@avg_hold_shares", stock.avg_hold_shares);
                cmd.Parameters.AddWithValue("@roe_weighted", stock.roe_weighted);
                cmd.Parameters.AddWithValue("@total_shares", stock.total_shares);
                cmd.Parameters.AddWithValue("@circulating_shares", stock.circulating_shares);
                cmd.Parameters.AddWithValue("@revenue", stock.revenue);
                cmd.Parameters.AddWithValue("@revenue_yoy", stock.revenue_yoy);
                cmd.Parameters.AddWithValue("@operating_profit", stock.operating_profit);
                cmd.Parameters.AddWithValue("@investment_income", stock.investment_income);
                cmd.Parameters.AddWithValue("@total_profit", stock.total_profit);
                cmd.Parameters.AddWithValue("@net_profit", stock.net_profit);
                cmd.Parameters.AddWithValue("@net_profit_yoy", stock.net_profit_yoy);
                cmd.Parameters.AddWithValue("@undistributed_profit", stock.undistributed_profit);
                cmd.Parameters.AddWithValue("@undistributed_profit_per_share", stock.undistributed_profit_per_share);
                cmd.Parameters.AddWithValue("@gross_profit_margin", stock.gross_profit_margin);
                cmd.Parameters.AddWithValue("@total_assets", stock.total_assets);
                cmd.Parameters.AddWithValue("@current_assets", stock.current_assets);
                cmd.Parameters.AddWithValue("@fixed_assets", stock.fixed_assets);
                cmd.Parameters.AddWithValue("@intangible_assets", stock.intangible_assets);
                cmd.Parameters.AddWithValue("@total_liabilities", stock.total_liabilities);
                cmd.Parameters.AddWithValue("@current_liabilities", stock.current_liabilities);
                cmd.Parameters.AddWithValue("@long_term_liabilities", stock.long_term_liabilities);
                cmd.Parameters.AddWithValue("@debt_ratio", stock.debt_ratio);
                cmd.Parameters.AddWithValue("@main_net_inflow", stock.main_net_inflow);
                cmd.Parameters.AddWithValue("@call_auction", stock.call_auction);
                cmd.Parameters.AddWithValue("@ultra_large_inflow", stock.ultra_large_inflow);
                cmd.Parameters.AddWithValue("@ultra_large_outflow", stock.ultra_large_outflow);
                cmd.Parameters.AddWithValue("@ultra_large_net", stock.ultra_large_net);
                cmd.Parameters.AddWithValue("@ultra_large_net_ratio", stock.ultra_large_net_ratio);
                cmd.Parameters.AddWithValue("@large_inflow", stock.large_inflow);
                cmd.Parameters.AddWithValue("@large_outflow", stock.large_outflow);
                cmd.Parameters.AddWithValue("@large_net", stock.large_net);
                cmd.Parameters.AddWithValue("@large_net_ratio", stock.large_net_ratio);
                cmd.Parameters.AddWithValue("@medium_inflow", stock.medium_inflow);
                cmd.Parameters.AddWithValue("@medium_outflow", stock.medium_outflow);
                cmd.Parameters.AddWithValue("@medium_net", stock.medium_net);
                cmd.Parameters.AddWithValue("@medium_net_ratio", stock.medium_net_ratio);
                cmd.Parameters.AddWithValue("@small_inflow", stock.small_inflow);
                cmd.Parameters.AddWithValue("@small_outflow", stock.small_outflow);
                cmd.Parameters.AddWithValue("@small_net", stock.small_net);
                cmd.Parameters.AddWithValue("@small_net_ratio", stock.small_net_ratio);
                cmd.Parameters.AddWithValue("@ddx_today", stock.ddx_today);
                cmd.Parameters.AddWithValue("@ddy_today", stock.ddy_today);
                cmd.Parameters.AddWithValue("@ddz_today", stock.ddz_today);
                cmd.Parameters.AddWithValue("@ddx_5d", stock.ddx_5d);
                cmd.Parameters.AddWithValue("@ddy_5d", stock.ddy_5d);
                cmd.Parameters.AddWithValue("@ddx_10d", stock.ddx_10d);
                cmd.Parameters.AddWithValue("@ddy_10d", stock.ddy_10d);
                cmd.Parameters.AddWithValue("@ddx_red_days", stock.ddx_red_days);
                cmd.Parameters.AddWithValue("@ddx_red_days_5d", stock.ddx_red_days_5d);
                cmd.Parameters.AddWithValue("@ddx_red_days_10d", stock.ddx_red_days_10d);
                cmd.Parameters.AddWithValue("@industry", stock.industry);
                cmd.Parameters.AddWithValue("@sector_leader_stock", stock.sector_leader_stock);
                cmd.Parameters.AddWithValue("@region_sector", stock.region_sector);
                cmd.Parameters.AddWithValue("@pe_static", stock.pe_static);
                cmd.Parameters.AddWithValue("@pe_ttm", stock.pe_ttm);
                cmd.Parameters.AddWithValue("@update_date", stock.update_date);
                await cmd.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"创建数据库时出错: {ex.Message}");
        }
    }
}