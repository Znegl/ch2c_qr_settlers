using Microsoft.EntityFrameworkCore.Migrations;

namespace Test2.Migrations
{
    public partial class addedactioncounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfActions",
                table: "Teams",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfActions",
                table: "Teams");
        }
    }
}
