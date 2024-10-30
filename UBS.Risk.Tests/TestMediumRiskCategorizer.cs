using UBS.Risk.Categorizer;
using UBS.Risk.Categorizer.Strategies;
using UBS.Risk.Common;

namespace UBS.Risk.Tests;

public class TestMediumRiskCategorizer
{
    [Fact]
    public void TestMediumRiskStrategy_1000000_Public()
    {
        ITradeCategorizer categorizer = new MediumRiskCategorizer();
        TradeCategorizerParameters parameters = new();
        TradeCategory category = categorizer.Categorize(new Trade(1_000_000, "Public", DateTime.Today), parameters);
        Assert.Equal(TradeCategory.NONE, category);
    }

    [Fact]
    public void TestMediumRiskStrategy_1000001_Public()
    {
        ITradeCategorizer categorizer = new MediumRiskCategorizer();
        TradeCategorizerParameters parameters = new();
        TradeCategory category = categorizer.Categorize(new Trade(1_000_001, "Public", DateTime.Today), parameters);
        Assert.Equal(TradeCategory.NONE, category);
    }

    [Fact]
    public void TestMediumRiskStrategy_999999_Public()
    {
        ITradeCategorizer categorizer = new MediumRiskCategorizer();
        TradeCategorizerParameters parameters = new();
        TradeCategory category = categorizer.Categorize(new Trade(999_999, "Public", DateTime.Today), parameters);
        Assert.Equal(TradeCategory.NONE, category);
    }

    [Fact]
    public void TestMediumRiskStrategy_1000000_Private()
    {
        ITradeCategorizer categorizer = new MediumRiskCategorizer();
        TradeCategorizerParameters parameters = new();
        TradeCategory category = categorizer.Categorize(new Trade(1_000_000, "Private", DateTime.Today), parameters);
        Assert.Equal(TradeCategory.NONE, category);
    }

    [Fact]
    public void TestMediumRiskStrategy_1000001_Private()
    {
        ITradeCategorizer categorizer = new MediumRiskCategorizer();
        TradeCategorizerParameters parameters = new();
        TradeCategory category = categorizer.Categorize(new Trade(1_000_001, "Private", DateTime.Today), parameters);
        Assert.Equal(TradeCategory.MEDIUMRISK, category);
    }

    [Fact]
    public void TestMediumRiskStrategy_999999_Private()
    {
        ITradeCategorizer categorizer = new MediumRiskCategorizer();
        TradeCategorizerParameters parameters = new();
        TradeCategory category = categorizer.Categorize(new Trade(999_999, "Private", DateTime.Today), parameters);
        Assert.Equal(TradeCategory.NONE, category);
    }
}