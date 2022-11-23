using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderAPI.Migrations
{
    public partial class Initial_20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "BasketProductes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    Payment_State = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketProductes_OrderId",
                table: "BasketProductes",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProductes_Orders_OrderId",
                table: "BasketProductes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProductes_Orders_OrderId",
                table: "BasketProductes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_BasketProductes_OrderId",
                table: "BasketProductes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "BasketProductes");
        }
    }
}
