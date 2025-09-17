using JiucaiAnalysisSystem.Common.Entity;
using JiucaiAnalysisSystem.Common.Utilities;
using MySqlConnector;
using SqlSugar;

namespace JiucaiAnalysisSystem.Core.DB;

public class MySqlDb
{
    private static readonly string ConnectionString =
        $"server={ConfigManager.DbHost};Database={ConfigManager.DbName};Uid={ConfigManager.DbUser};Pwd={ConfigManager.DbPassword};Port={ConfigManager.DbPort};CharSet=utf8mb4";

    //创建数据库对象 (用法和EF Dappper一样通过new保证线程安全)
    public static SqlSugarClient Db;

    public static bool InitDb()
    {
        try
        {
            // 1. 尝试连接数据库，如果失败则去建库
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open(); // 尝试打开，如果库不存在会失败
                }
            }
            catch
            {
                // 2. 如果库不存在，连到 mysql 库去建库
                var builder = new MySqlConnectionStringBuilder(ConnectionString);
                var tmpConnStr =
                    $"server={builder.Server};port={builder.Port};user={builder.UserID};password={builder.Password};database=mysql;";

                using (var conn = new MySqlConnection(tmpConnStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            $"CREATE DATABASE IF NOT EXISTS `{ConfigManager.DbName}` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            InitSqlDb();

            //建表（看文档迁移）
            Db.CodeFirst.InitTables<EastMoneyStock>(); //所有库都支持 
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
            return false;
        }
    }

    private static void InitSqlDb()
    {
        Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            },
            db =>
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    //获取原生SQL推荐 5.1.4.63  性能OK
                    Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

                    //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                    //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer,sql,pars))
                };
                //注意多租户 有几个设置几个
                //db.GetConnection(i).Aop
            });
    }
}