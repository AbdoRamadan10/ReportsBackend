using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsBackend.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddingStudentDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FILTER",
                table: "REPORTCOLUMNS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PRICE",
                table: "PRODUCTS",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.CreateTable(
                name: "STUDENTDETAILS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    STUDENTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EMERGENCYCONTACTNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    EMERGENCYCONTACTPHONE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    HEALTHINFORMATION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ADDITIONALNOTES = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ACADEMICADVISOR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENTDETAILS", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDENTDETAILS");

            migrationBuilder.AlterColumn<bool>(
                name: "FILTER",
                table: "REPORTCOLUMNS",
                type: "NUMBER(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

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
