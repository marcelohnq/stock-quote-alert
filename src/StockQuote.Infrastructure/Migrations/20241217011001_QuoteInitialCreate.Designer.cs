﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockQuote.Infrastructure.Data;

#nullable disable

namespace StockQuote.Infrastructure.Migrations
{
    [DbContext(typeof(QuoteContext))]
    [Migration("20241217011001_QuoteInitialCreate")]
    partial class QuoteInitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StockQuote.Core.QuoteAggregate.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("StockQuote.Core.QuoteAggregate.QuotePrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("QuoteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuoteId");

                    b.ToTable("QuotePrice");
                });

            modelBuilder.Entity("StockQuote.Core.QuoteAggregate.Quote", b =>
                {
                    b.OwnsOne("StockQuote.Core.BaseClasses.Audit", "Audit", b1 =>
                        {
                            b1.Property<int>("QuoteId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("QuoteId");

                            b1.ToTable("Quotes");

                            b1.WithOwner()
                                .HasForeignKey("QuoteId");
                        });

                    b.OwnsOne("StockQuote.Core.QuoteAggregate.Asset", "Asset", b1 =>
                        {
                            b1.Property<int>("QuoteId")
                                .HasColumnType("int");

                            b1.Property<string>("Ticker")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("QuoteId");

                            b1.ToTable("Quotes");

                            b1.WithOwner()
                                .HasForeignKey("QuoteId");
                        });

                    b.OwnsOne("StockQuote.Core.QuoteAggregate.LimitAlert", "LimitAlert", b1 =>
                        {
                            b1.Property<int>("QuoteId")
                                .HasColumnType("int");

                            b1.Property<decimal>("Down")
                                .HasPrecision(18, 4)
                                .HasColumnType("decimal(18,4)");

                            b1.Property<decimal>("Up")
                                .HasPrecision(18, 4)
                                .HasColumnType("decimal(18,4)");

                            b1.HasKey("QuoteId");

                            b1.ToTable("Quotes");

                            b1.WithOwner()
                                .HasForeignKey("QuoteId");
                        });

                    b.Navigation("Asset")
                        .IsRequired();

                    b.Navigation("Audit")
                        .IsRequired();

                    b.Navigation("LimitAlert")
                        .IsRequired();
                });

            modelBuilder.Entity("StockQuote.Core.QuoteAggregate.QuotePrice", b =>
                {
                    b.HasOne("StockQuote.Core.QuoteAggregate.Quote", null)
                        .WithMany("Prices")
                        .HasForeignKey("QuoteId");

                    b.OwnsOne("StockQuote.Core.QuoteAggregate.Price", "Price", b1 =>
                        {
                            b1.Property<int>("QuotePriceId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("datetime2");

                            b1.Property<decimal>("Value")
                                .HasPrecision(18, 4)
                                .HasColumnType("decimal(18,4)");

                            b1.HasKey("QuotePriceId");

                            b1.ToTable("QuotePrice");

                            b1.WithOwner()
                                .HasForeignKey("QuotePriceId");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("StockQuote.Core.QuoteAggregate.Quote", b =>
                {
                    b.Navigation("Prices");
                });
#pragma warning restore 612, 618
        }
    }
}