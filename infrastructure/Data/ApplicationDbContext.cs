using Microsoft.EntityFrameworkCore;
using core.Utils;
using core.Model;

namespace infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<StockAction> Actions { get; set; }
    public DbSet<PortfolioElement> PortfolioElements { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ForexRate> ForexRates { get; set; }
    public DbSet<OrderBook> OrderBooks { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var orderbooks = new List<OrderBook>
        {
            new OrderBook
            {
                Id = 1,
                StockActionId = 1
            }
        };

        var actions = new List<StockAction>
        {
            new StockAction
            {
                Id = 1,
                Isin = "ISIN1",
                Title = "Action 1",
                Symbol = "SYM1",
                ListingMarket = "Market 1",
                Index = "CAC40",
                TradingCurrency = TradingCurrency.EUR,
                Quantity = 100,
                Misc = new { SomeProperty = "Value" },
                MarketPrice = 100,
            },
            new StockAction
            {
                Id = 2,
                Isin = "ISIN2",
                Title = "Action 2",
                Symbol = "SYM2",
                ListingMarket = "Market 2",
                Index = "SBF120",
                TradingCurrency = TradingCurrency.EUR,
                Quantity = 100,
                Misc = new { SomeProperty = "Value" },
                MarketPrice = 50,
            },
            new StockAction
            {
                Id = 3,
                Isin = "ISIN3",
                Title = "Action 3",
                Symbol = "SYM3",
                ListingMarket = "Market 3",
                Index = "NASDAQ100",
                TradingCurrency = TradingCurrency.USD,
                Quantity = 100,
                Misc = new { SomeProperty = "Value" },
                MarketPrice = 100,
            },
        };
        var customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                AccountValue = 10000.0,
            },
            new Customer
            {
                Id = 2,
                AccountValue = 5000.0,
            },
        };

        var forexRates = new List<ForexRate>
        {
            new ForexRate
            {
                Id = 1,
                TradingCurrency = TradingCurrency.EUR,
                Rate = 1.0
            },
            new ForexRate
            {
                Id = 2,
                TradingCurrency = TradingCurrency.USD,
                Rate = 1.05
            }
        };

        var orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                OrderBookId = 1,
                OrderType = OrderType.Buy,
                Quantity = 20,
                Price = 98,
                CustomerId = 2,
            },
            new Order
            {
                Id = 3,
                OrderBookId = 1,
                OrderType = OrderType.Buy,
                Quantity = 20,
                Price = 99,
                CustomerId = 1,
            },
            new Order
            {
                Id = 2,
                OrderBookId = 1,
                OrderType = OrderType.Sell,
                Quantity = 15,
                Price = 101,
                CustomerId = 2,
            },
            new Order
            {
                Id = 4,
                OrderBookId = 1,
                OrderType = OrderType.Sell,
                Quantity = 10,
                Price = 103,
                CustomerId = 2,
            }
        };

        modelBuilder.Entity<Customer>().HasData(customers);
        modelBuilder.Entity<ForexRate>().HasData(forexRates);
        modelBuilder.Entity<OrderBook>().HasData(orderbooks);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<StockAction>().HasData(actions);
    }
}