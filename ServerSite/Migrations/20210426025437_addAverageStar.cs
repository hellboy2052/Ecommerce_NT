using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerSite.Migrations
{
    public partial class addAverageStar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Average",
                table: "Rates");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageStar",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageStar",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "Average",
                table: "Rates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
