using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealsApp.Migrations
{
    /// <inheritdoc />
    public partial class MyInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deals_products_Productid",
                table: "deals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_scores",
                table: "scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deals",
                table: "deals");

            migrationBuilder.RenameTable(
                name: "scores",
                newName: "Scores");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "deals",
                newName: "Deals");

            migrationBuilder.RenameIndex(
                name: "IX_deals_Productid",
                table: "Deals",
                newName: "IX_Deals_Productid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deals",
                table: "Deals",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Products_Productid",
                table: "Deals",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Products_Productid",
                table: "Deals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deals",
                table: "Deals");

            migrationBuilder.RenameTable(
                name: "Scores",
                newName: "scores");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Deals",
                newName: "deals");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_Productid",
                table: "deals",
                newName: "IX_deals_Productid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_scores",
                table: "scores",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deals",
                table: "deals",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_deals_products_Productid",
                table: "deals",
                column: "Productid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
