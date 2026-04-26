using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Configuration_PromotionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PromotionItems_PromotionId",
                table: "PromotionItems");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionItems_PromotionId_ProductUnitId",
                table: "PromotionItems",
                columns: new[] { "PromotionId", "ProductUnitId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PromotionItems_PromotionId_ProductUnitId",
                table: "PromotionItems");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionItems_PromotionId",
                table: "PromotionItems",
                column: "PromotionId");
        }
    }
}
