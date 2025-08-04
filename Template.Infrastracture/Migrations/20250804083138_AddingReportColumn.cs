using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsBackend.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddingReportColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATATYPE",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "DISPLAYNAME",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "NAME",
                table: "REPORTCOLUMNS");

            migrationBuilder.AddColumn<string>(
                name: "FIELD",
                table: "REPORTCOLUMNS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FILTER",
                table: "REPORTCOLUMNS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FLOATINGFILTER",
                table: "REPORTCOLUMNS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HEADERNAME",
                table: "REPORTCOLUMNS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HIDE",
                table: "REPORTCOLUMNS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RESIZABLE",
                table: "REPORTCOLUMNS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ROWGROUP",
                table: "REPORTCOLUMNS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SORTABLE",
                table: "REPORTCOLUMNS",
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
                name: "FIELD",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "FILTER",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "FLOATINGFILTER",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "HEADERNAME",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "HIDE",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "RESIZABLE",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "ROWGROUP",
                table: "REPORTCOLUMNS");

            migrationBuilder.DropColumn(
                name: "SORTABLE",
                table: "REPORTCOLUMNS");

            migrationBuilder.AddColumn<string>(
                name: "DATATYPE",
                table: "REPORTCOLUMNS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DISPLAYNAME",
                table: "REPORTCOLUMNS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NAME",
                table: "REPORTCOLUMNS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");

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
