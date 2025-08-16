using JiucaiAnalysisSystem.Common.Utilities;
using MySqlConnector;

namespace JiucaiAnalysisSystem.Core.DB;

public class DbCommand
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public static readonly string ConnectionString =
        $"Server={ConfigManager.DbHost};Uid={ConfigManager.DbUser};Pwd={ConfigManager.DbPassword};SslMode=None;";

    public static bool ExecuteCommand(string sql)
    {
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
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
            using (var connection = new MySqlConnection(ConnectionString))
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
            using (var connection = new MySqlConnection(ConnectionString))
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