using UBS.Risk.Common;

namespace UBS.Risk.Categorizer.Strategies;

public class LowRiskCategorizer : ITradeCategorizer
{
    /// <summary>
    /// A trade is categorized as Low Risk when the value is greater than 1,000,000 and the client sector is Public.
    /// </summary>
    /// <param name="trade">An ITrade.</param>
    /// <param name="parameters">Parameters for categorizing an ITrade.</param>
    /// <returns>TradeCategory.LOWRISK if it meets the criteria, TradeCategory.NONE otherwise.</returns>
    public TradeCategory Categorize(ITrade trade, TradeCategorizerParameters parameters)
    {
        if (trade.Value > 1_000_000 && trade.ClientSector == ClientSector.Public)
        {
            return TradeCategory.LOWRISK;
        }

        return TradeCategory.NONE;
    }
}