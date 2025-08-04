using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsBackend.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddingStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PRICE",
                table: "PRODUCTS",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.CreateTable(
                name: "STUDENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GRADE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GENDER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DATEOFBIRTH = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PHONENUMBER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ADDRESS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENTS", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDENTS");

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
