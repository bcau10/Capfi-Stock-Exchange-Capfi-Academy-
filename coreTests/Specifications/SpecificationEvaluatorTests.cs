using core.Model;
using core.Specifications;
using core.Utils;
using coreTests.TestUtils;

namespace coreTests.Specifications;

[TestFixture]
public class SpecificationEvaluatorTests
{
    [Test]
    public void GetQuery_ShouldApplyCriteria_WhenSpecHasCriteria()
    {
        // Given
        var stockActions = new List<StockAction>
        {
            new() { Id = 1 },
            new() { Id = 2 },
            new() { Id = 3 }
        };
        var queryable = stockActions.AsQueryable();
        var specification = new SomeSpecification(1);

        // When
        var result = SpecificationEvaluator<StockAction>.GetQuery(queryable, specification);
        
        // Then
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(1));
        });
    }

    [Test]
    public void GetQuery_ShouldApplyOrderBy_WhenSpecHasOrderBy()
    {
        // Given
        var stockActions = new List<StockAction>
        {
            new() { Id = 1 },
            new() { Id = 2 },
            new() { Id = 3 }
        };
        var queryable = stockActions.AsQueryable();
        var specification = new SomeSpecification();

        // When
        var result = SpecificationEvaluator<StockAction>.GetQuery(queryable, specification);

        // Then
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.First().Id, Is.EqualTo(1));
            Assert.That(result.Last().Id, Is.EqualTo(3));
        });
    }

    [Test]
    public void GetQuery_ShouldApplyPaging_WhenSpecIsPagingEnabled()
    {
        // Given
        var stockActions = new List<StockAction>
        {
            new() { Id = 1 },
            new() { Id = 2 },
            new() { Id = 3 }
        };
        var queryable = stockActions.AsQueryable();        var specification = new SomeSpecification(1, 2);

        // When
        var result = SpecificationEvaluator<StockAction>.GetQuery(queryable, specification);

        // Then
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(3));
            Assert.That(result.Last().Id, Is.EqualTo(3));
        });
    }
    
    private static StockAction CreateStockAction(int id) => new()
    {
        Id = id,
        Isin = $"ISIN{id}",
        Title = $"Action {id}",
        Symbol = $"SYM{id}",
        ListingMarket = $"Market {id}",
        Index = "CAC40",
        TradingCurrency = TradingCurrency.EUR,
        Quantity = 100,
        Misc = new { SomeProperty = "Value" },
        MarketPrice = 92.32
    };
}