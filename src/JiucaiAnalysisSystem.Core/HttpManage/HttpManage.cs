using JiucaiAnalysisSystem.Common.Entity;
using JiucaiAnalysisSystem.Common.Utilities;
using Newtonsoft.Json;
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
            for (int page = 1; page <= 10; page++)
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
    static async Task<List<EastMoneyStock>> GetHistoryForDate(string code, string startDate, string? endDate = null)
    {
        try
        {
            var list = new List<EastMoneyStock>();
            using var client = new HttpClient();
            string secid = (code.StartsWith("6") ? "1." : "0.") + code;
            string url =
                $"https://push2.eastmoney.com/api/qt/ulist/get?fltt=1&invt=2&fields=f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f14,f15,f16,f17,f18,f20,f21,f22,f23,f24,f25,f26,f30,f33,f34,f35,f36,f37,f38,f39,f40,f41,f42,f43,f44,f45,f46,f47,f48,f49,f50,f51,f52,f53,f54,f55,f56,f57,f62,f63,f64,f65,f66,f69,f70,f71,f72,f75,f76,f77,f78,f81,f82,f83,f84,f87,f88,f89,f90,f91,f92,f94,f95,f97,f98,f99,f100,f101,f102,f114,f115,f221&secids={secid}&pn=1&np=1&pz=10&dect=1&beg={startDate.Replace("-", "")}&end={(endDate ?? startDate).Replace("-", "")}";

            Console.WriteLine($"url:\t{url}\n{DateTime.Now:O}");
            var json = await client.GetStringAsync(url);
            Console.WriteLine($"code:\t{code}\n{DateTime.Now:O}");
            await Task.Delay(300);
            var data = JObject.Parse(json)["data"]?["diff"];
            if (data == null) return list;
            foreach (var item in data)
            {
                var eastMoneyStock = JsonConvert.DeserializeObject<EastMoneyStock>(item.ToString());
                if (eastMoneyStock != null)
                {
                    list.Add(eastMoneyStock);
                }
            }
            Console.WriteLine($"list.Count:\t{list.Count}\n{DateTime.Now:O}");

            return list;
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
    public static async Task<List<EastMoneyStock>> GetHistoryForDate(string date)
    {
        var codes = await GetAllStockCodes();
        var list = new List<EastMoneyStock>();
        // 使用LINQ分组，每10个一组，并拼接
        var result = codes
            .Select((value, index) => new { value, index })
            .GroupBy(x => x.index / 10)
            .Select(g => string.Join(",", g.Select(x => x.value)))
            .ToList();
        foreach (var code in result)
        {
            await Task.Delay(300);
            var data = await GetHistoryForDate(code, date);
            if (data != null && data.Any())
            {
                list.AddRange(data);
            }
        }

        return list;
    }
}