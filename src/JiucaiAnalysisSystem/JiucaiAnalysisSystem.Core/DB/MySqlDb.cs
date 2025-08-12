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
        AppConfig config = ConfigManager.LoadConfig();
        // 连接字符串不包含数据库名，因为我们要检查数据库是否存在
        string connectionString = $"Server={config.DbHost};Uid={config.DbUser};Pwd={config.DbPassword};SslMode=None;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // 查询数据库是否存在
                string checkQuery =
                    $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{config.DbName}'";

                using (var command = new MySqlCommand(checkQuery, connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
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
        AppConfig config = ConfigManager.LoadConfig();
        string connectionString = $"Server={config.DbHost};Uid={config.DbUser};Pwd={config.DbPassword};SslMode=None;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // 创建数据库的SQL命令
                string createDbBaseQuery =
                    $"CREATE DATABASE IF NOT EXISTS {config.DbName} CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci";

                using (var command = new MySqlCommand(createDbBaseQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"数据库 '{config.DbName}' 创建成功！");
                }

                // 创建数据库的SQL命令
                string createTableQuery =
                    $"CREATE TABLE `{config.DbName}`.`stock_history` (id BIGINT AUTO_INCREMENT PRIMARY KEY COMMENT '主键ID',trade_date DATE NOT NULL COMMENT '交易日期',stock_code VARCHAR(10) NOT NULL COMMENT '股票代码',stock_name VARCHAR(50) NOT NULL COMMENT '股票名称',open_price DECIMAL(10,2) NOT NULL COMMENT '开盘价',close_price DECIMAL(10,2) NOT NULL COMMENT '收盘价',high_price DECIMAL(10,2) NOT NULL COMMENT '最高价',low_price DECIMAL(10,2) NOT NULL COMMENT '最低价',change_amount DECIMAL(10,2) COMMENT '涨跌额',change_rate DECIMAL(6,2) COMMENT '涨跌幅(%)',volume BIGINT COMMENT '成交量(手)',turnover DECIMAL(18,2) COMMENT '成交额(元)',amplitude DECIMAL(6,2) COMMENT '振幅(%)',turnover_rate DECIMAL(6,2) COMMENT '换手率(%)',pe_ratio DECIMAL(10,2) COMMENT '市盈率(动态)',pb_ratio DECIMAL(10,2) COMMENT '市净率',total_market_cap DECIMAL(20,2) COMMENT '总市值',circulating_market_cap DECIMAL(20,2) COMMENT '流通市值',UNIQUE KEY uk_date_code (trade_date, stock_code),INDEX idx_code_date (stock_code, trade_date)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='A股股票历史行情数据'";

                using (var command = new MySqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"数据库 '{config.DbName}' 创建成功！");
                    return true;
                }
            }
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
        }
        else
        {
            Console.WriteLine($"数据库 '{config.DbName}' 不存在，正在创建...");
            CreateDatabase();
        }
    }
}