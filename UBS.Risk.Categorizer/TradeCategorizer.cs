using UBS.Risk.Common;

namespace UBS.Risk.Categorizer;

/// <summary>
/// Represents a trade categorizer that aggregates multiple strategies to categorize a trade.
/// </summary>
public class TradeCategorizer
{
	private readonly ITradeCategorizer[] _strategies;

	/// <summary>
	/// Initializes a new instance of the <see cref="TradeCategorizer"/> class with the specified strategies.
	/// This Categorizer will use the strategies in the order they are provided.
	/// </summary>
	/// <param name="strategies">List of strategies that will be used to calculate the risk.</param>
	public TradeCategorizer(ITradeCategorizer[] strategies)
	{
		_strategies = strategies;
	}

	/// <summary>
	/// Calculates the risk of a trade given the parameters.
	/// </summary>
	/// <param name="trade">The Trade we want to calculate risk.</param>
	/// <param name="parameters">Any parameter that's necessary to caculate the risk.</param>
	/// <returns>The calculated risk or None if there is no risk.</returns>
	public TradeCategory Categorize(ITrade trade, TradeCategorizerParameters parameters)
	{
		foreach (var strategy in _strategies)
		{
			var risk = strategy.Categorize(trade, parameters);
			if (risk != TradeCategory.NONE)
			{
				return risk;
			}
		}

		return TradeCategory.NONE;
	}
}