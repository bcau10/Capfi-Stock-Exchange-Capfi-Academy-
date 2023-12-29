using core.Model;
using core.Repositories;
using core.Specifications;
using core.Utils;
using infrastructure.Data;
using infrastructure.Repositories;
using infrastructureTests.TestUtils;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace infrastructureTests.Repositories;

[TestFixture]
public class GenericRepositoryTests
{
    private ApplicationDbContext _context;
    private IGenericRepository<StockAction> _repository;

    [OneTimeSetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "fakeDb")
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new GenericRepository<StockAction>(_context);
    }
    
    [TearDown]
    public void TearDown()
    {
        _context.Set<StockAction>().RemoveRange(_context.Set<StockAction>());
    }

    [Test]
    public async Task GetByIdAsync_GivenExistingId_ShouldReturnEntity()
    {
        // Given
        var entity = CreateStockAction(1);
        await _context.Set<StockAction>().AddAsync(entity);
        await _context.SaveChangesAsync();

        // When
        var result = await _repository.GetByIdAsync(entity.Id);

        // Then
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(entity.Id));
    }

    [Test]
    public async Task GetListAllAsync_ShouldReturnAllEntities()
    {
        // Given
        var entities = new List<StockAction>
        {
            CreateStockAction(1),
            CreateStockAction(2)
        };

        await _context.Set<StockAction>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        // When
        var result = await _repository.GetListAllAsync();

        // Then
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(entities.Count));
        Assert.That(result.Select(e => e.Id), Is.EquivalentTo(entities.Select(e => e.Id)));
    }

    [Test]
    public async Task CountAsync_ShouldReturnCountBasedOnSpec()
    {
        // Given
        var spec = Substitute.For<ISpecification<StockAction>>();
        spec.Criteria.Returns(e => e.Id > 0); 

        var entities = new List<StockAction>
        {
            CreateStockAction(1),
            CreateStockAction(2),
            CreateStockAction(3)
        };

        _context.Set<StockAction>().AddRange(entities);
        await _context.SaveChangesAsync();

        // When
        var result = await _repository.CountAsync(new SomeSpecification());

        // Then
        Assert.That(result, Is.EqualTo(3)); 
    }
    
    [TestCase(1)]
    [TestCase(2)]
    public async Task GetEntityWithSpec_ShouldReturnEntityBasedOnSpec(int id)
    {
        // Given
        var spec = new SomeSpecification(id);
        var entities = new List<StockAction>
        {
            CreateStockAction(1),
            CreateStockAction(2),
            CreateStockAction(3)
        };
    
        _context.Set<StockAction>().AddRange(entities);
        await _context.SaveChangesAsync();
    
        // When
        var result = await _repository.GetEntityWithSpec(spec);
    
        // Then
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(id));
    }
    
    [TestCase(5)]
    [TestCase(-1)]
    public async Task GetEntityWithSpec_ShouldReturnNothing_WhenSpecFails(int id)
    {
        // Given
        var spec = new SomeSpecification(id);
        var entities = new List<StockAction>
        {
            CreateStockAction(1),
            CreateStockAction(2),
            CreateStockAction(3)
        };
    
        _context.Set<StockAction>().AddRange(entities);
        await _context.SaveChangesAsync();
    
        // When
        var result = await _repository.GetEntityWithSpec(spec);
    
        // Then
        Assert.That(result, Is.Null);
    }
    
    [TestCase("I")]
    [TestCase("ISI")]
    public async Task GetListWithSpecAsync_ShouldReturnListBasedOnSpec(string contains)
    {
        // Given
        var spec = new SomeSpecification(contains);
        var entities = new List<StockAction>
        {
            CreateStockAction(1),
            CreateStockAction(2),
            CreateStockAction(3)
        };
    
        _context.Set<StockAction>().AddRange(entities);
        await _context.SaveChangesAsync();
    
        // When
        var result = await _repository.GetListWithSpecAsync(spec);
    
        // Then
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(3));
    }
    
    [TestCase("Z")]
    [TestCase("ZAKARIA")]
    public async Task GetListWithSpecAsync_ShouldReturnEmptyList_WhenSpecFails(string contains)
    {
        // Given
        var spec = new SomeSpecification(contains);
        var entities = new List<StockAction>
        {
            CreateStockAction(1),
            CreateStockAction(2),
            CreateStockAction(3)
        };
    
        _context.Set<StockAction>().AddRange(entities);
        await _context.SaveChangesAsync();
    
        // When
        var result = await _repository.GetListWithSpecAsync(spec);
    
        // Then
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(0));
    }

    private class SomeSpecification : BaseSpecification<StockAction>
    {
        public SomeSpecification(int id) : base(e => e.Id == id)
        {
        }
        
        public SomeSpecification(string contains) : base(e => e.Isin.Contains(contains))
        {
        }
        
        public SomeSpecification()
        {
            
        }
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