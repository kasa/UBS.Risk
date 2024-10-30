using UBS.Risk.Categorizer;
using UBS.Risk.Categorizer.Strategies;
using UBS.Risk.Common;

namespace UBS.Risk.Tests;

public class TestExpiredTradeCategorizer
{
    [Fact]
    public void TestExpiredTradeStrategy_D29()
    {
        DateTime referenceDate = new(2020, 12, 11);
        ITradeCategorizer categorizer = new ExpiredTradeCategorizer();
        TradeCategorizerParameters parameters = new TradeCategorizerParameters { ReferenceDate = referenceDate };
        TradeCategory category = categorizer.Categorize(new Trade(1_000_000, "Public", referenceDate.AddDays(29)), parameters);
        Assert.Equal(TradeCategory.NONE, category);
    }

    [Fact]
    public void TestExpiredTradeStrategy_D30()
    {
        DateTime referenceDate = new(2020, 12, 11);
        ITradeCategorizer categorizer = new ExpiredTradeCategorizer();
        TradeCategorizerParameters parameters = new TradeCategorizerParameters { ReferenceDate = referenceDate };
        TradeCategory category = categorizer.Categorize(new Trade(1_000_000, "Public", referenceDate.AddDays(30)), parameters);
        Assert.Equal(TradeCategory.NONE, category);
    }

    [Fact]
    public void TestExpiredTradeStrategy_D31()
    {
        DateTime referenceDate = new(2020, 12, 11);
        ITradeCategorizer categorizer = new ExpiredTradeCategorizer();
        TradeCategorizerParameters parameters = new TradeCategorizerParameters { ReferenceDate = referenceDate };
        TradeCategory category = categorizer.Categorize(new Trade(1_000_000, "Public", referenceDate.AddDays(31)), parameters);
        Assert.Equal(TradeCategory.NONE, category);
    }
}