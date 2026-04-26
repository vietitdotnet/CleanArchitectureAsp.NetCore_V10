using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Check_Constraints_v_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PromotionItems",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddCheckConstraint(
                name: "CK_PromotionItem_OriginalPrice",
                table: "PromotionItems",
                sql: "[OriginalPrice] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_PromotionItem_Override_Incomplete",
                table: "PromotionItems",
                sql: "([OverrideValue] IS NULL AND [IsPercentageOverride] IS NULL) OR ([OverrideValue] IS NOT NULL AND [IsPercentageOverride] IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_PromotionItem_OverrideValue_Logic",
                table: "PromotionItems",
                sql: "([OverrideValue] IS NULL) OR \r\n                  ([IsPercentageOverride] = 1 AND [OverrideValue] >= 0 AND [OverrideValue] <= 100) OR \r\n                  ([IsPercentageOverride] = 0 AND [OverrideValue] >= 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_PromotionItem_OriginalPrice",
                table: "PromotionItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_PromotionItem_Override_Incomplete",
                table: "PromotionItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_PromotionItem_OverrideValue_Logic",
                table: "PromotionItems");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PromotionItems",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}
