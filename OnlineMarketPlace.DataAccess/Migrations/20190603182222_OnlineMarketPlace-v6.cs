using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.DataAccess.Migrations
{
    public partial class OnlineMarketPlacev6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "TotalWeight",
                table: "OrderProducts",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderProducts",
                newName: "TotalWeight");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderProducts",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
