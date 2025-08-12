using JiucaiAnalysisSystem.Common.Entity;
using JiucaiAnalysisSystem.Common.Utilities;
using Newtonsoft.Json.Linq;

namespace JiucaiAnalysisSystem.Core.HttpManage;

public class HttpManage
{
    /// <summary>
    /// 获取所有股票代码
    /// </summary>
    /// <returns></returns>
    private static async Task<List<string>> GetAllStockCodes()
    {
        var codes = new List<string>();
        try
        {
            using var client = new HttpClient();
            for (int page = 1; page <= 2; page++)
            {
                string url =
                    $"https://push2.eastmoney.com/api/qt/clist/get?np=1&fltt=1&invt=2&fs=m:0+t:6,m:0+t:80,m:1+t:2,m:1+t:23,m:0+t:81+s:2048&fields=f12&fid=f3&pn={page}&pz=100&po=1&dect=1";
                var json = await client.GetStringAsync(url);
                var data = JObject.Parse(json)["data"]?["diff"];
                await Task.Delay(300);
                if (data == null) break;
                foreach (var item in data)
                {
                    string code = item["f12"]?.ToString();
                    if (!string.IsNullOrEmpty(code))
                        codes.Add(code);
                }
            }

            return codes;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return codes;
        }
    }

    /// <summary>
    /// 获取股票某一日的历史数据
    /// </summary>
    /// <param name="code">股票代码</param>
    /// <param name="date">日期</param>
    /// <returns></returns>
    static async Task<StockData?> GetHistoryForDate(string code, string date)
    {
        try
        {
            using var client = new HttpClient();
            string secid = (code.StartsWith("6") ? "1." : "0.") + code;
            string url =
                $"https://push2.eastmoney.com/api/qt/ulist/get?fltt=1&invt=2&fields=f1,f2,f3,f4,f5,f6,f51,f52,f53,f54,f55,f56,f57&secids={secid}&pn=1&np=1&pz=20&dect=1&beg={date.Replace("-", "")}&end={date.Replace("-", "")}";
            var json = await client.GetStringAsync(url);
            var klines = JObject.Parse(json)["data"]?["diff"];
            if (klines != null && klines.HasValues)
            {
                var arr = klines[0]?.ToString().Split(',');
                return new StockData
                {
                    stock_code = code,
                    radte_date = date,
                    close_price =  decimal.Parse(arr[2]),
                };
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Logger.Error(e.Message);
            return null;
        }
    }

    /// <summary>
    /// 获取所有股票某一日的历史数据
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static async Task<List<StockData>> GetHistoryForDate(string date)
    {
        var stockDatas = new List<StockData>();
        var codes = await GetAllStockCodes();
        foreach (var code in codes)
        {
            await Task.Delay(300);
            var data = await GetHistoryForDate(code, date);
            if (data != null)
            {
                stockDatas.Add(data);
            }
        }

        return stockDatas;
    }
}