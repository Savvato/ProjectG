using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectG.OrderService.Infrastructure.Db.Migrations
{
    public partial class AddedOrderStatusDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStatusDetails",
                columns: table => new
                {
                    OrderId = table.Column<long>(nullable: false),
                    IsBasketCleared = table.Column<bool>(nullable: false),
                    AreProductsReserved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusDetails", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderStatusDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStatusDetails");
        }
    }
}
