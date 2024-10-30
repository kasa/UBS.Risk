using UBS.Risk.Common;

namespace UBS.Risk.Categorizer;

/// <summary>
/// Interface for strategies that categorize a trade.
/// </summary>
public interface ITradeCategorizer
{
    TradeCategory Categorize(ITrade trade, TradeCategorizerParameters parameters);
}