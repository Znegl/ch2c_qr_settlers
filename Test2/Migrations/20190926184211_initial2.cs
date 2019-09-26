using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test2.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemsToBuys",
                columns: table => new
                {
                    ItemsToBuyID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Guid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsToBuys", x => x.ItemsToBuyID);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogLineID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(nullable: true),
                    LogTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogLineID);
                });

            migrationBuilder.CreateTable(
                name: "ResourceReads",
                columns: table => new
                {
                    ResourceReadID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamID = table.Column<int>(nullable: false),
                    ResourceUUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceReads", x => x.ResourceReadID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamName = table.Column<string>(nullable: true),
                    Metal = table.Column<int>(nullable: false),
                    Wood = table.Column<int>(nullable: false),
                    Cloth = table.Column<int>(nullable: false),
                    Plastic = table.Column<int>(nullable: false),
                    StarTtime = table.Column<DateTime>(nullable: false),
                    NumberOfActions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "TeamHasBoughts",
                columns: table => new
                {
                    TeamHasBoughtID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamID = table.Column<int>(nullable: false),
                    Bought = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamHasBoughts", x => x.TeamHasBoughtID);
                    table.ForeignKey(
                        name: "FK_TeamHasBoughts_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamHasBoughts_TeamID",
                table: "TeamHasBoughts",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsToBuys");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "ResourceReads");

            migrationBuilder.DropTable(
                name: "TeamHasBoughts");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
