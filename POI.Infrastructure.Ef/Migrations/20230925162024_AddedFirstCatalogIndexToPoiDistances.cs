using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POI.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddedFirstCatalogIndexToPoiDistances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PoiDistances_FirstPoiCatalogId",
                table: "PoiDistances",
                column: "FirstPoiCatalogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PoiDistances_FirstPoiCatalogId",
                table: "PoiDistances");
        }
    }
}
