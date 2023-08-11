using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealsApp.Migrations
{
    /// <inheritdoc />
    public partial class MyInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Scores",
                newName: "Productid");

            migrationBuilder.RenameColumn(
                name: "deal_id",
                table: "Scores",
                newName: "Dealid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Productid",
                table: "Scores",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "Dealid",
                table: "Scores",
                newName: "deal_id");
        }
    }
}
