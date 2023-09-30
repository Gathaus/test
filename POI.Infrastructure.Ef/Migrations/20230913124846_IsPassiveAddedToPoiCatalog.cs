using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POI.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class IsPassiveAddedToPoiCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPassive",
                table: "PoiCatalogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPassive",
                table: "PoiCatalogs");
        }
    }
}
