using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsBackend.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class ModifyReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DASH_REPORTS_DASH_PRIVILEGES_PRIVILEGEID",
                table: "DASH_REPORTS");

            migrationBuilder.AlterColumn<string>(
                name: "QUERY",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<int>(
                name: "PRIVILEGEID",
                table: "DASH_REPORTS",
                type: "NUMBER(10)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<string>(
                name: "PATH",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "FILTER",
                table: "DASH_REPORTCOLUMNS",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AddColumn<int>(
                name: "SORT",
                table: "DASH_REPORTCOLUMNS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRICE",
                table: "DASH_PRODUCTS",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_DASH_REPORTS_DASH_PRIVILEGES_PRIVILEGEID",
                table: "DASH_REPORTS",
                column: "PRIVILEGEID",
                principalTable: "DASH_PRIVILEGES",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DASH_REPORTS_DASH_PRIVILEGES_PRIVILEGEID",
                table: "DASH_REPORTS");

            migrationBuilder.DropColumn(
                name: "SORT",
                table: "DASH_REPORTCOLUMNS");

            migrationBuilder.AlterColumn<string>(
                name: "QUERY",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PRIVILEGEID",
                table: "DASH_REPORTS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PATH",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FILTER",
                table: "DASH_REPORTCOLUMNS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRICE",
                table: "DASH_PRODUCTS",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AddForeignKey(
                name: "FK_DASH_REPORTS_DASH_PRIVILEGES_PRIVILEGEID",
                table: "DASH_REPORTS",
                column: "PRIVILEGEID",
                principalTable: "DASH_PRIVILEGES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
