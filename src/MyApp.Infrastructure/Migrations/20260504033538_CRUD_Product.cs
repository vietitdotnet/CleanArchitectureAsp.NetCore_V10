using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CRUD_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductUnits_ProductId",
                table: "ProductUnits");

            migrationBuilder.DropColumn(
                name: "IsBaseUnit",
                table: "ProductUnits");

            migrationBuilder.DropColumn(
                name: "LowStockThreshold",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_ProductId",
                table: "ProductUnits",
                column: "ProductId",
                unique: true,
                filter: "[ConversionRate] = 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductUnit_ConversionRate",
                table: "ProductUnits",
                sql: "[ConversionRate] > 0 AND [ConversionRate] <= 100000000");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductUnit_SellingPrice",
                table: "ProductUnits",
                sql: "[SellingPrice] > 0 AND [SellingPrice] <= 10000000000");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_BasePrice",
                table: "Products",
                sql: "[BasePrice] > 0 AND [BasePrice] <= 10000000000");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_CostPrice",
                table: "Products",
                sql: "[CostPrice] > 0 AND [CostPrice] <= 100000000");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductUnits_ProductId",
                table: "ProductUnits");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductUnit_ConversionRate",
                table: "ProductUnits");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductUnit_SellingPrice",
                table: "ProductUnits");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_BasePrice",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_CostPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsBaseUnit",
                table: "ProductUnits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "LowStockThreshold",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_ProductId",
                table: "ProductUnits",
                column: "ProductId",
                unique: true,
                filter: "[IsBaseUnit] = 1");
        }
    }
}
