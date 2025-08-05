using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsBackend.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddingReportDetailProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DETAILID",
                table: "REPORTS",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HASDETAIL",
                table: "REPORTS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRICE",
                table: "PRODUCTS",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DETAILID",
                table: "REPORTS");

            migrationBuilder.DropColumn(
                name: "HASDETAIL",
                table: "REPORTS");

            migrationBuilder.AlterColumn<decimal>(
                name: "PRICE",
                table: "PRODUCTS",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
        }
    }
}
