using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POI.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIndexsToPoiDistance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PoiDistances_FirstPoiCatalogId",
                table: "PoiDistances");

            migrationBuilder.DropIndex(
                name: "IX_PoiDistances_SecondPoiCatalogId",
                table: "PoiDistances");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PoiDistances_FirstPoiCatalogId",
                table: "PoiDistances",
                column: "FirstPoiCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_PoiDistances_SecondPoiCatalogId",
                table: "PoiDistances",
                column: "SecondPoiCatalogId");
        }
    }
}
