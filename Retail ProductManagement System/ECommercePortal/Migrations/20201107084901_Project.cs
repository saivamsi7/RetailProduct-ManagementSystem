using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommercePortal.Migrations
{
    public partial class Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Zipcode = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<string>(nullable: true),
                    VendorId = table.Column<int>(nullable: false),
                    VendorName = table.Column<string>(nullable: true),
                    DeliveryCharge = table.Column<double>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    ExpectedDateOfDelivery = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Rating = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
