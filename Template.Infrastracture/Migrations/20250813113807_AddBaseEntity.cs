using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsBackend.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDAT",
                table: "DASH_REPORTS",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CREATEDBY",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DELETEDAT",
                table: "DASH_REPORTS",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DELETEDBY",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: true);


            migrationBuilder.AddColumn<bool>(
                name: "ISDELETED",
                table: "DASH_REPORTS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UPDATEDAT",
                table: "DASH_REPORTS",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UPDATEDBY",
                table: "DASH_REPORTS",
                type: "NVARCHAR2(2000)",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "CREATEDAT",
                table: "DASH_REPORTS");

            migrationBuilder.DropColumn(
                name: "CREATEDBY",
                table: "DASH_REPORTS");

            migrationBuilder.DropColumn(
                name: "DELETEDAT",
                table: "DASH_REPORTS");

            migrationBuilder.DropColumn(
                name: "DELETEDBY",
                table: "DASH_REPORTS");

            migrationBuilder.DropColumn(
                name: "ISDELETED",
                table: "DASH_REPORTS");

            migrationBuilder.DropColumn(
                name: "UPDATEDAT",
                table: "DASH_REPORTS");

            migrationBuilder.DropColumn(
                name: "UPDATEDBY",
                table: "DASH_REPORTS");

        }
    }
}
