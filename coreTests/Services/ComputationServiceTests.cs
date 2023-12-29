using core.Model;
using core.Services;

namespace coreTests.Services;

[TestFixture]
public class ComputationServiceTests
{
    private ComputationService _computationService;

    [SetUp]
    public void Setup()
    {
        _computationService = new ComputationService();
    }

    [TestCase(100.0, 1.2, 83.33)]
    [TestCase(150.0, 1.0, 150.0)]
    public void PriceToEur_ShouldCalculatePriceInEur(double marketPrice, double rate, double expectedResult)
    {
        // Given
        var forexRate = new ForexRate { Rate = rate };

        // When
        var result = _computationService.PriceToEur(marketPrice, forexRate);

        // Then
        Assert.That(result, Is.EqualTo(expectedResult).Within(0.01));
    }

    [TestCase(2000.0, 1500.0, true)] 
    [TestCase(1000.0, 1500.0, false)]
    public void IsSolvable_ShouldReturnCorrectResult(double accountValue, double priceOrder, bool expectedResult)
    {
        // Given
        var customer = new Customer { AccountValue = accountValue };

        // When
        var result = _computationService.IsSolvable(customer, priceOrder);

        // Then
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase(100, 50, true)] // portfolioQuantity, quantity, expected result
    [TestCase(30, 50, false)] // portfolioQuantity, quantity, expected result
    public void IsValid_ShouldReturnCorrectResult(int portfolioQuantity, double quantity, bool expectedResult)
    {
        // Given
        var portfolioElement = new PortfolioElement { PortfolioQuantity = portfolioQuantity };

        // When
        var result = _computationService.IsValid(portfolioElement, quantity);

        // Then
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}