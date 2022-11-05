using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergyPlatformProgram.Repository.Migrations
{
    public partial class FixIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Consumtion",
                table: "Consumtion",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consumtion",
                table: "Consumtion");
        }
    }
}
