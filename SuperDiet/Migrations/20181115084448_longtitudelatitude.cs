using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperDiet.Migrations
{
    public partial class longtitudelatitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Branch",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "Branch",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Branch");
        }
    }
}
