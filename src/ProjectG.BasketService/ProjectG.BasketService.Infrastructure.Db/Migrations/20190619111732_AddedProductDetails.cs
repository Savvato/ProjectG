using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectG.BasketService.Infrastructure.Db.Migrations
{
    public partial class AddedProductDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "BasketPositions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "BasketPositions",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BasketPositions_CustomerId_ProductId",
                table: "BasketPositions",
                columns: new[] { "CustomerId", "ProductId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BasketPositions_CustomerId_ProductId",
                table: "BasketPositions");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "BasketPositions");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "BasketPositions");
        }
    }
}
