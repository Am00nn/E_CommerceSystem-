using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceSystem.Migrations
{
    /// <inheritdoc />
    public partial class opdatorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Orderdate",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.AddColumn<int>(
                name: "ProductP_Id",
                table: "OrderProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductP_Id",
                table: "OrderProducts",
                column: "ProductP_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductP_Id",
                table: "OrderProducts",
                column: "ProductP_Id",
                principalTable: "Products",
                principalColumn: "P_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductP_Id",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_ProductP_Id",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ProductP_Id",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "Orderdate");
        }
    }
}
