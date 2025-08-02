using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsBackend.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRIVILEGES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PATH = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRIVILEGES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRICE = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SCREENS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PATH = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCREENS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PASSWORDHASH = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "REPORTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QUERY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PATH = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRIVILEGEID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REPORTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REPORTS_PRIVILEGES_PRIVILEGEID",
                        column: x => x.PRIVILEGEID,
                        principalTable: "PRIVILEGES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ROLESCREENS",
                columns: table => new
                {
                    ROLEID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SCREENID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLESCREENS", x => new { x.SCREENID, x.ROLEID });
                    table.ForeignKey(
                        name: "FK_ROLESCREENS_ROLES_ROLEID",
                        column: x => x.ROLEID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROLESCREENS_SCREENS_SCREENID",
                        column: x => x.SCREENID,
                        principalTable: "SCREENS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERROLES",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ROLEID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERROLES", x => new { x.USERID, x.ROLEID });
                    table.ForeignKey(
                        name: "FK_USERROLES_ROLES_ROLEID",
                        column: x => x.ROLEID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERROLES_USERS_USERID",
                        column: x => x.USERID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REPORTCOLUMNS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DISPLAYNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DATATYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    REPORTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REPORTCOLUMNS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REPORTCOLUMNS_REPORTS_REPORTID",
                        column: x => x.REPORTID,
                        principalTable: "REPORTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REPORTPARAMETERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DISPLAYNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DATATYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PARAMETERTYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ISREQUIRED = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DEFAULTVALUE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QUERYFORDROPDOWN = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    REPORTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REPORTPARAMETERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REPORTPARAMETERS_REPORTS_REPORTID",
                        column: x => x.REPORTID,
                        principalTable: "REPORTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ROLEREPORTS",
                columns: table => new
                {
                    ROLEID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    REPORTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLEREPORTS", x => new { x.REPORTID, x.ROLEID });
                    table.ForeignKey(
                        name: "FK_ROLEREPORTS_REPORTS_REPORTID",
                        column: x => x.REPORTID,
                        principalTable: "REPORTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROLEREPORTS_ROLES_ROLEID",
                        column: x => x.ROLEID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_REPORTCOLUMNS_REPORTID",
                table: "REPORTCOLUMNS",
                column: "REPORTID");

            migrationBuilder.CreateIndex(
                name: "IX_REPORTPARAMETERS_REPORTID",
                table: "REPORTPARAMETERS",
                column: "REPORTID");

            migrationBuilder.CreateIndex(
                name: "IX_REPORTS_PRIVILEGEID",
                table: "REPORTS",
                column: "PRIVILEGEID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLEREPORTS_ROLEID",
                table: "ROLEREPORTS",
                column: "ROLEID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLESCREENS_ROLEID",
                table: "ROLESCREENS",
                column: "ROLEID");

            migrationBuilder.CreateIndex(
                name: "IX_USERROLES_ROLEID",
                table: "USERROLES",
                column: "ROLEID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "REPORTCOLUMNS");

            migrationBuilder.DropTable(
                name: "REPORTPARAMETERS");

            migrationBuilder.DropTable(
                name: "ROLEREPORTS");

            migrationBuilder.DropTable(
                name: "ROLESCREENS");

            migrationBuilder.DropTable(
                name: "USERROLES");

            migrationBuilder.DropTable(
                name: "REPORTS");

            migrationBuilder.DropTable(
                name: "SCREENS");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "PRIVILEGES");
        }
    }
}
