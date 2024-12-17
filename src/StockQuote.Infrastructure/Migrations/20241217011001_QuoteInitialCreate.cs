using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockQuote.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuoteInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asset_Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimitAlert_Up = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    LimitAlert_Down = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotePrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price_Value = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Price_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotePrice_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotePrice_QuoteId",
                table: "QuotePrice",
                column: "QuoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotePrice");

            migrationBuilder.DropTable(
                name: "Quotes");
        }
    }
}
