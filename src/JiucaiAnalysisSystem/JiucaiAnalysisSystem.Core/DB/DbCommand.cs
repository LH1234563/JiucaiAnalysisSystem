using JiucaiAnalysisSystem.Common.Utilities;
using MySqlConnector;

namespace JiucaiAnalysisSystem.Core.DB;

public class DbCommand
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    /// <returns></returns>
    public static string GetConnectionString()
    {
        AppConfig config = ConfigManager.LoadConfig();
        // 连接字符串
        return $"Server={config.DbHost};Uid={config.DbUser};Pwd={config.DbPassword};SslMode=None;";
    }

    public static bool ExecuteCommand(string sql)
    {
        try
        {
            string connectionString = GetConnectionString();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
            return false;
        }
    }

    public static bool ExecuteNonQueryCommand(string sql)
    {
        try
        {
            string connectionString = GetConnectionString();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
            return false;
        }
    }
    public static bool ExecuteNonQueryCommand(List<string> sqls)
    {
        try
        {
            string connectionString = GetConnectionString();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                foreach (var sql in sqls)
                {
                    using var command = new MySqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
            return false;
        }
    }
}