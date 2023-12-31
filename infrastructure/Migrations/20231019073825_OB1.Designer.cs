﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using infrastructure.Data;

#nullable disable

namespace infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231019073825_OB1")]
    partial class OB1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("core.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AccountValue")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountValue = 10000.0
                        },
                        new
                        {
                            Id = 2,
                            AccountValue = 5000.0
                        });
                });

            modelBuilder.Entity("core.Model.ForexRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Rate")
                        .HasColumnType("REAL");

                    b.Property<int>("TradingCurrency")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ForexRates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Rate = 1.0,
                            TradingCurrency = 8364
                        },
                        new
                        {
                            Id = 2,
                            Rate = 1.05,
                            TradingCurrency = 36
                        });
                });

            modelBuilder.Entity("core.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderBookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderType")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderBookId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 2,
                            OrderBookId = 1,
                            OrderType = 0,
                            Price = 98.0,
                            Quantity = 20
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 1,
                            OrderBookId = 1,
                            OrderType = 0,
                            Price = 99.0,
                            Quantity = 20
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            OrderBookId = 1,
                            OrderType = 1,
                            Price = 101.0,
                            Quantity = 15
                        },
                        new
                        {
                            Id = 4,
                            CustomerId = 2,
                            OrderBookId = 1,
                            OrderType = 1,
                            Price = 103.0,
                            Quantity = 10
                        });
                });

            modelBuilder.Entity("core.Model.OrderBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("StockActionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StockActionId")
                        .IsUnique();

                    b.ToTable("OrderBooks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StockActionId = 1
                        });
                });

            modelBuilder.Entity("core.Model.PortfolioElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActionId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("BuyPrice")
                        .HasColumnType("REAL");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PortfolioQuantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("CustomerId");

                    b.ToTable("PortfolioElements");
                });

            modelBuilder.Entity("core.Model.StockAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Index")
                        .HasColumnType("TEXT");

                    b.Property<string>("Isin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ListingMarket")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<double>("MarketPrice")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("TEXT");

                    b.Property<int>("TradingCurrency")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Actions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Index = "CAC40",
                            Isin = "ISIN1",
                            ListingMarket = "Market 1",
                            MarketPrice = 100.0,
                            Quantity = 100,
                            Symbol = "SYM1",
                            Title = "Action 1",
                            TradingCurrency = 8364
                        },
                        new
                        {
                            Id = 2,
                            Index = "SBF120",
                            Isin = "ISIN2",
                            ListingMarket = "Market 2",
                            MarketPrice = 50.0,
                            Quantity = 100,
                            Symbol = "SYM2",
                            Title = "Action 2",
                            TradingCurrency = 8364
                        },
                        new
                        {
                            Id = 3,
                            Index = "NASDAQ100",
                            Isin = "ISIN3",
                            ListingMarket = "Market 3",
                            MarketPrice = 100.0,
                            Quantity = 100,
                            Symbol = "SYM3",
                            Title = "Action 3",
                            TradingCurrency = 36
                        });
                });

            modelBuilder.Entity("core.Model.Order", b =>
                {
                    b.HasOne("core.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("core.Model.OrderBook", "OrderBook")
                        .WithMany("OrderElements")
                        .HasForeignKey("OrderBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("OrderBook");
                });

            modelBuilder.Entity("core.Model.OrderBook", b =>
                {
                    b.HasOne("core.Model.StockAction", "StockAction")
                        .WithOne("OrderBook")
                        .HasForeignKey("core.Model.OrderBook", "StockActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StockAction");
                });

            modelBuilder.Entity("core.Model.PortfolioElement", b =>
                {
                    b.HasOne("core.Model.StockAction", "StockAction")
                        .WithMany()
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("core.Model.Customer", "Customer")
                        .WithMany("PortfolioElements")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("StockAction");
                });

            modelBuilder.Entity("core.Model.Customer", b =>
                {
                    b.Navigation("PortfolioElements");
                });

            modelBuilder.Entity("core.Model.OrderBook", b =>
                {
                    b.Navigation("OrderElements");
                });

            modelBuilder.Entity("core.Model.StockAction", b =>
                {
                    b.Navigation("OrderBook");
                });
#pragma warning restore 612, 618
        }
    }
}
