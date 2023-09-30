using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POI.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class PoiCatalogForeignKeysAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PoiCatalogs_CityId",
                table: "PoiCatalogs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PoiCatalogs_CountryId",
                table: "PoiCatalogs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PoiCatalogs_CountyId",
                table: "PoiCatalogs",
                column: "CountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PoiCatalogs_City_CityId",
                table: "PoiCatalogs",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PoiCatalogs_Country_CountryId",
                table: "PoiCatalogs",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PoiCatalogs_County_CountyId",
                table: "PoiCatalogs",
                column: "CountyId",
                principalTable: "County",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoiCatalogs_City_CityId",
                table: "PoiCatalogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PoiCatalogs_Country_CountryId",
                table: "PoiCatalogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PoiCatalogs_County_CountyId",
                table: "PoiCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_PoiCatalogs_CityId",
                table: "PoiCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_PoiCatalogs_CountryId",
                table: "PoiCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_PoiCatalogs_CountyId",
                table: "PoiCatalogs");
        }
    }
}
