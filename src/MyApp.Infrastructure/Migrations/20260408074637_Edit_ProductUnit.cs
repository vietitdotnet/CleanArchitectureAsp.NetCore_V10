using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Edit_ProductUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionItems_Products_ProductId",
                table: "PromotionItems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "PromotionItems",
                newName: "ProductUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionItems_ProductId",
                table: "PromotionItems",
                newName: "IX_PromotionItems_ProductUnitId");

            migrationBuilder.AddColumn<string>(
                name: "UnitBarcode",
                table: "PromotionItems",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "PromotionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionItems_ProductUnits_ProductUnitId",
                table: "PromotionItems",
                column: "ProductUnitId",
                principalTable: "ProductUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionItems_ProductUnits_ProductUnitId",
                table: "PromotionItems");

            migrationBuilder.DropColumn(
                name: "UnitBarcode",
                table: "PromotionItems");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "PromotionItems");

            migrationBuilder.RenameColumn(
                name: "ProductUnitId",
                table: "PromotionItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionItems_ProductUnitId",
                table: "PromotionItems",
                newName: "IX_PromotionItems_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionItems_Products_ProductId",
                table: "PromotionItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
