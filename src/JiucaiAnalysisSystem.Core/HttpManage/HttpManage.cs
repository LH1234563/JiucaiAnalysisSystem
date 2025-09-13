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
    public static async Task<List<string>> GetAllStockCodes()
    {
        var codes = new List<string>();
        try
        {
            using var client = new HttpClient();

            Console.WriteLine($"GetAllStockCodes\t{DateTime.Now:O}");
            var page = 0;
            while (true)
            {
                page++;
                string url =
                    $"https://push2.eastmoney.com/api/qt/clist/get?" +
                    $"pn={page}&pz=100&po=1&np=1&fltt=2&fid=f3&fs=m%3A0%2Bt%3A6%2Cm%3A0%2Bt%3A80%2Cm%3A1%2Bt%3A2%2Cm%3A1%2Bt%3A23%2Cm%3A0%2Bt%3A81%2Bs%3A2048&fields=f12";
                var json = await client.GetStringAsync(url);
                var data = JObject.Parse(json)["data"]?["diff"];
                if (data == null) break;
                int count = 0;
                foreach (var item in data)
                {
                    string code = item["f12"]?.ToString();
                    if (!string.IsNullOrEmpty(code))
                        codes.Add(code);
                    count++;
                }

                if (count < 100)
                {
                    break;
                }

                await Task.Delay(300);
            }

            Console.WriteLine($"GetAllStockCodes\t{DateTime.Now:O}");
            return codes;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return codes;
        }
    }

    /// <summary>
    /// 获取所有交易日历-历史数据
    /// </summary>
    /// <returns></returns>
    // public static async Task<List<DateTime>> GetAllTradeTdate()
    // {
    //     var codes = new List<DateTime>();
    //     try
    //     {
    //         using var client = new HttpClient();
    //
    //         Console.WriteLine($"GetAllTradeTdate\t{DateTime.Now:O}");
    //         var page = 0;
    //         while (true)
    //         {
    //             page++;
    //             string url =
    //                 $"https://finance.sina.com.cn/realstock/company/klc_td_sh.txt";
    //             var json = await client.GetStringAsync(url);
    //             var data = JObject.Parse(json)["data"]?["diff"];
    //             if (data == null) break;
    //             int count = 0;
    //             foreach (var item in data)
    //             {
    //                 string code = item["f12"]?.ToString();
    //                 if (!string.IsNullOrEmpty(code))
    //                     codes.Add(code);
    //                 count++;
    //             }
    //
    //             if (count < 100)
    //             {
    //                 break;
    //             }
    //
    //             await Task.Delay(300);
    //         }
    //
    //         Console.WriteLine($"GetAllStockCodes\t{DateTime.Now:O}");
    //         return codes;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return codes;
    //     }
    // }

    /// <summary>
    /// 获取股票某一日的历史数据
    /// </summary>
    /// <param name="code">股票代码</param>
    /// <param name="date">日期</param>
    /// <returns></returns>
    static async Task<List<EastMoneyStock>> GetHistoryForDate(string code, string startDate)
    {
        try
        {
            var list = new List<EastMoneyStock>();
            using var client = new HttpClient();
            // string secid = (code.StartsWith("6") ? "1." : "0.") + code;
            string url =
                $"https://push2.eastmoney.com/api/qt/ulist/get?fltt=1&invt=2&fields=f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f14,f15,f16,f17,f18,f20,f21,f22,f23,f24,f25,f26,f30,f33,f34,f35,f36,f37,f38,f39,f40,f41,f42,f43,f44,f45,f46,f47,f48,f49,f50,f51,f52,f53,f54,f55,f56,f57,f62,f63,f64,f65,f66,f69,f70,f71,f72,f75,f76,f77,f78,f81,f82,f83,f84,f87,f88,f89,f90,f91,f92,f94,f95,f97,f98,f99,f100,f101,f102,f114,f115,f221&secids={code}&pn=1&np=1&pz=150&dect=1&beg={startDate.Replace("-", "")}&end={startDate.Replace("-", "")}";

            var json = await client.GetStringAsync(url);
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
        var codes = ConfigManager.StockCodeAll;
        var list = new List<EastMoneyStock>();
        // 使用LINQ分组，每10个一组，并拼接
        var result = codes
            .Select(x => x.StartsWith("6") ? "1." + x : "0." + x)
            .Select((value, index) => new { value, index })
            .GroupBy(x => x.index / 150)
            .Select(g => string.Join(",", g.Select(x => x.value)))
            .ToList();
        foreach (var code in result)
        {
            await Task.Delay(300);
            var data = await GetHistoryForDate(code, date);
            if (data != null && data.Any())
            {
                list.AddRange(data.Select(a =>
                {
                    a.CurrentDate = date;
                    return a;
                }));

                Console.WriteLine($"已获取list.Count:\t{list.Count}条\t{DateTime.Now:O}");
            }
        }

        return list;
    }
}