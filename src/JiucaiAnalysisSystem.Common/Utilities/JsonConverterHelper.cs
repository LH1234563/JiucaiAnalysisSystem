using System.Globalization;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace JiucaiAnalysisSystem.Common.Utilities;

public class DecimalDashToZeroConverter : JsonConverter<decimal>
{
    public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        // 如果是字符串类型
        if (reader.TokenType == JsonToken.String)
        {
            var str = (string)reader.Value;

            // 如果是 "-"，转换为 0
            if (str == "-")
                return 0m;

            // 尝试解析科学计数法格式
            if (decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
        }

        // 如果是数字类型，直接返回
        if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
        {
            return Convert.ToDecimal(reader.Value);
        }

        // 默认返回 0
        return 0m;
    }

    public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }
}

public class DateTimeCustomConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormat = "yyyyMMdd"; // 自定义的日期格式

    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.Value != null)
        {
            var str = reader.Value.ToString();

            // 尝试将字符串按指定的格式转换为 DateTime
            if (DateTime.TryParseExact(str, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out var date))
            {
                return date;
            }
        }

        return DateTime.MinValue;
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        // 如果需要将 DateTime 转回字符串，可以按同样的格式输出
        writer.WriteValue(value.ToString(_dateFormat));
    }
}

public class LongDashConverter : JsonConverter<long>
{
    public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        // 如果遇到字符串
        if (reader.TokenType == JsonToken.String)
        {
            var str = (string)reader.Value;

            // 如果字符串为 "-"，返回 0
            if (str == "-")
                return 0;
        }

        // 如果是数字类型，直接转换为 long
        if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
        {
            return Convert.ToInt64(reader.Value);
        }

        // 默认返回 0
        return 0;
    }

    public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }
}

public class IntDashConverter : JsonConverter<int>
{
    public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        // 如果遇到字符串
        if (reader.TokenType == JsonToken.String)
        {
            var str = (string)reader.Value;

            // 如果字符串为 "-"，返回 0
            if (str == "-")
                return 0;
        }

        // 如果是数字类型，直接转换为 iny
        if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
        {
            return int.TryParse(reader.Value?.ToString(), out var result) ? result : 0;
        }

        // 默认返回 0
        return 0;
    }

    public override void WriteJson(JsonWriter writer, int value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }
}