using System;
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
                name: "DASH_PRIVILEGES",
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
                    table.PrimaryKey("PK_DASH_PRIVILEGES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DASH_PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PRICE = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    CATEGORYID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CATEGORYNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EXPIRATIONDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DASH_PRODUCTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DASH_ROLES",
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
                    table.PrimaryKey("PK_DASH_ROLES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DASH_SCREENS",
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
                    table.PrimaryKey("PK_DASH_SCREENS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DASH_STUDENTDETAILS",
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
                    table.PrimaryKey("PK_DASH_STUDENTDETAILS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DASH_STUDENTS",
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
                    table.PrimaryKey("PK_DASH_STUDENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DASH_USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    USERNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PASSWORDHASH = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DASH_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DASH_REPORTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QUERY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PATH = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HASDETAIL = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DETAILID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DETAILCOLUMN = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PRIVILEGEID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DASH_REPORTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DASH_REPORTS_DASH_PRIVILEGES_PRIVILEGEID",
                        column: x => x.PRIVILEGEID,
                        principalTable: "DASH_PRIVILEGES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DASH_ROLESCREENS",
                columns: table => new
                {
                    ROLEID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SCREENID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DASH_ROLESCREENS", x => new { x.SCREENID, x.ROLEID });
                    table.ForeignKey(
                        name: "FK_DASH_ROLESCREENS_DASH_ROLES_ROLEID",
                        column: x => x.ROLEID,
                        principalTable: "DASH_ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DASH_ROLESCREENS_DASH_SCREENS_SCREENID",
                        column: x => x.SCREENID,
                        principalTable: "DASH_SCREENS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DASH_USERROLES",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ROLEID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DASH_USERROLES", x => new { x.USERID, x.ROLEID });
                    table.ForeignKey(
                        name: "FK_DASH_USERROLES_DASH_ROLES_ROLEID",
                        column: x => x.ROLEID,
                        principalTable: "DASH_ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DASH_USERROLES_DASH_USERS_USERID",
                        column: x => x.USERID,
                        principalTable: "DASH_USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DASH_REPORTCOLUMNS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FIELD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    HEADERNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SORTABLE = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    FILTER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RESIZABLE = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    FLOATINGFILTER = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ROWGROUP = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    HIDE = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ISMASTER = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    REPORTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DASH_REPORTCOLUMNS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DASH_REPORTCOLUMNS_DASH_REPORTS_REPORTID",
                        column: x => x.REPORTID,
                        principalTable: "DASH_REPORTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DASH_REPORTPARAMETERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DISPLAYNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DATATYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PARAMETERTYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ISREQUIRED = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DEFAULTVALUE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QUERYFORDROPDOWN = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SORT = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    REPORTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DASH_REPORTPARAMETERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DASH_REPORTPARAMETERS_DASH_REPORTS_REPORTID",
                        column: x => x.REPORTID,
                        principalTable: "DASH_REPORTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DASH_ROLEREPORTS",
                columns: table => new
                {
                    ROLEID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    REPORTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DASH_ROLEREPORTS", x => new { x.REPORTID, x.ROLEID });
                    table.ForeignKey(
                        name: "FK_DASH_ROLEREPORTS_DASH_REPORTS_REPORTID",
                        column: x => x.REPORTID,
                        principalTable: "DASH_REPORTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DASH_ROLEREPORTS_DASH_ROLES_ROLEID",
                        column: x => x.ROLEID,
                        principalTable: "DASH_ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DASH_REPORTCOLUMNS_REPORTID",
                table: "DASH_REPORTCOLUMNS",
                column: "REPORTID");

            migrationBuilder.CreateIndex(
                name: "IX_DASH_REPORTPARAMETERS_REPORTID",
                table: "DASH_REPORTPARAMETERS",
                column: "REPORTID");

            migrationBuilder.CreateIndex(
                name: "IX_DASH_REPORTS_PRIVILEGEID",
                table: "DASH_REPORTS",
                column: "PRIVILEGEID");

            migrationBuilder.CreateIndex(
                name: "IX_DASH_ROLEREPORTS_ROLEID",
                table: "DASH_ROLEREPORTS",
                column: "ROLEID");

            migrationBuilder.CreateIndex(
                name: "IX_DASH_ROLESCREENS_ROLEID",
                table: "DASH_ROLESCREENS",
                column: "ROLEID");

            migrationBuilder.CreateIndex(
                name: "IX_DASH_USERROLES_ROLEID",
                table: "DASH_USERROLES",
                column: "ROLEID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DASH_PRODUCTS");

            migrationBuilder.DropTable(
                name: "DASH_REPORTCOLUMNS");

            migrationBuilder.DropTable(
                name: "DASH_REPORTPARAMETERS");

            migrationBuilder.DropTable(
                name: "DASH_ROLEREPORTS");

            migrationBuilder.DropTable(
                name: "DASH_ROLESCREENS");

            migrationBuilder.DropTable(
                name: "DASH_STUDENTDETAILS");

            migrationBuilder.DropTable(
                name: "DASH_STUDENTS");

            migrationBuilder.DropTable(
                name: "DASH_USERROLES");

            migrationBuilder.DropTable(
                name: "DASH_REPORTS");

            migrationBuilder.DropTable(
                name: "DASH_SCREENS");

            migrationBuilder.DropTable(
                name: "DASH_ROLES");

            migrationBuilder.DropTable(
                name: "DASH_USERS");

            migrationBuilder.DropTable(
                name: "DASH_PRIVILEGES");
        }
    }
}
