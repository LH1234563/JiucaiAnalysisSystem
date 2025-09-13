namespace JiucaiAnalysisSystem.Common.Utilities;

public class MeasureHelper
{
    public static bool IsExistStockCodeAll()
    {
        if (ConfigManager.StockCodeAll != null && ConfigManager.StockCodeAll.Any() &&
            ConfigManager.StockCodeAll.Count > 5000)
        {
            return true;
        }

        return false;
    }
}