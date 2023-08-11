using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealsApp.Migrations
{
    /// <inheritdoc />
    public partial class initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Productid = table.Column<string>(type: "text", nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false),
                    Dealid = table.Column<string>(type: "text", nullable: false),
                    score_value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Productid = table.Column<string>(type: "text", nullable: false),
                    discount_percentage = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.id);
                    table.ForeignKey(
                        name: "FK_Deals_Products_Productid",
                        column: x => x.Productid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_Productid",
                table: "Deals",
                column: "Productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
