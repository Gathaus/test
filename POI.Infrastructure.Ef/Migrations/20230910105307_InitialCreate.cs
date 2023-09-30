using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace POI.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Demographies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cat1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cat2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cat3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cat4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cat5 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CityCode = table.Column<int>(type: "int", nullable: false),
                    CountyCode = table.Column<int>(type: "int", nullable: false),
                    DistrictCode = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalPopulation = table.Column<int>(type: "int", nullable: true),
                    MalePopulation = table.Column<int>(type: "int", nullable: true),
                    FemalePopulation = table.Column<int>(type: "int", nullable: true),
                    UrbanPopulation = table.Column<int>(type: "int", nullable: true),
                    RuralPopulation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demographies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoiCatalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameOnBoard = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: false),
                    CountyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Geography = table.Column<Point>(type: "geography", nullable: true),
                    PoiType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoiCatalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoiDistances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPoiCatalogId = table.Column<int>(type: "int", nullable: false),
                    SecondPoiCatalogId = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoiDistances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoiDistances_PoiCatalogs_FirstPoiCatalogId",
                        column: x => x.FirstPoiCatalogId,
                        principalTable: "PoiCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoiDistances_PoiCatalogs_SecondPoiCatalogId",
                        column: x => x.SecondPoiCatalogId,
                        principalTable: "PoiCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoiDistances_FirstPoiCatalogId",
                table: "PoiDistances",
                column: "FirstPoiCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_PoiDistances_SecondPoiCatalogId",
                table: "PoiDistances",
                column: "SecondPoiCatalogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Demographies");

            migrationBuilder.DropTable(
                name: "PoiDistances");

            migrationBuilder.DropTable(
                name: "PoiCatalogs");
        }
    }
}
