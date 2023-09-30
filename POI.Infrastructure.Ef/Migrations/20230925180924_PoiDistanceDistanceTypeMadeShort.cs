using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POI.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class PoiDistanceDistanceTypeMadeShort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Distance",
                table: "PoiDistances",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "PoiDistances",
                type: "float",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
