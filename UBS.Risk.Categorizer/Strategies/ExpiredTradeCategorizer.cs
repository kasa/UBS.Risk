using UBS.Risk.Common;

namespace UBS.Risk.Categorizer.Strategies;

public class ExpiredTradeCategorizer : ITradeCategorizer
{
    /// <summary>
    /// A trade is categorized as Expired when the NextPaymentDate is late by more than 30 days base on a ReferenceDate
    /// that will be given as a parameter.
    /// </summary>
    /// <param name="trade">An ITrade.</param>
    /// <param name="parameters">Parameters for categorizing an ITrade.</param>
    /// <returns>TradeCategory.EXPIRED if it meets the criteria, TradeCategory.NONE otherwise.</returns>
    public TradeCategory Categorize(ITrade trade, TradeCategorizerParameters parameters)
    {
        if (trade.NextPaymentDate.AddDays(30) < parameters.ReferenceDate)
        {
            return TradeCategory.EXPIRED;
        }

        return TradeCategory.NONE;
    }
}