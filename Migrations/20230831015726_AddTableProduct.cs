using Microsoft.EntityFrameworkCore.Migrations;

namespace Project3.Migrations
{
    public partial class AddTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, null, null, "Iphone 7", 300.0 },
                    { 2, 1, null, null, "Iphone 7s", 350.0 },
                    { 3, 1, null, null, "Iphone 8", 400.0 },
                    { 4, 1, null, null, "Iphone 8s", 420.0 },
                    { 5, 1, null, null, "Iphone 11", 600.0 },
                    { 6, 1, null, null, "Iphone 11s", 650.0 },
                    { 7, 1, null, null, "Iphone 13", 700.0 },
                    { 8, 1, null, null, "Iphone 13 Pro", 850.0 },
                    { 9, 1, null, null, "Iphone 14", 900.0 },
                    { 10, 1, null, null, "Iphone 14 Pro Max ", 1000.0 },
                    { 11, 2, null, null, "Ipad Mini", 350.0 },
                    { 12, 2, null, null, "Ipad Pro", 550.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
