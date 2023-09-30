using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace POI.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class PoiSalesPointMatchHistoryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoiSalesPointMatchHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoiId = table.Column<int>(type: "int", nullable: false),
                    SalesPointId = table.Column<int>(type: "int", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatchSource = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoiSalesPointMatchHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoiSalesPointMatchHistories_PoiCatalogs_PoiId",
                        column: x => x.PoiId,
                        principalTable: "PoiCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_PoiSalesPointMatchHistories_PoiId",
                table: "PoiSalesPointMatchHistories",
                column: "PoiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoiSalesPointMatchHistories");

        }
    }
}
