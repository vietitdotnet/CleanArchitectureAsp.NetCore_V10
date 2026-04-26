using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fash_sale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DailyEndTime",
                table: "Promotions",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DailyStartTime",
                table: "Promotions",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnlineOnly",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LimitQuantity",
                table: "Promotions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoldQuantity",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DiscountCode",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountType",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Promotions_DailyTime_Logic",
                table: "Promotions",
                sql: "\r\n                        ([DailyStartTime] IS NULL AND [DailyEndTime] IS NULL)\r\n                        OR\r\n                        ([DailyStartTime] IS NOT NULL AND [DailyEndTime] IS NOT NULL AND [DailyStartTime] < [DailyEndTime])\r\n                    ");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Promotions_FlashSale_Fields",
                table: "Promotions",
                sql: "\r\n                        ([Type] = 1) -- FlashSale\r\n                        OR\r\n                        ([Type] <> 1 AND \r\n                            [LimitQuantity] IS NULL AND \r\n                            [DailyStartTime] IS NULL AND \r\n                            [DailyEndTime] IS NULL\r\n                        )\r\n                    ");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Promotions_Quantity_Logic",
                table: "Promotions",
                sql: "\r\n                        ([LimitQuantity] IS NULL AND [SoldQuantity] >= 0)\r\n                        OR\r\n                        ([LimitQuantity] IS NOT NULL AND [SoldQuantity] >= 0 AND [SoldQuantity] <= [LimitQuantity])\r\n                     ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Promotions_DailyTime_Logic",
                table: "Promotions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Promotions_FlashSale_Fields",
                table: "Promotions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Promotions_Quantity_Logic",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DailyEndTime",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DailyStartTime",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "IsOnlineOnly",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "LimitQuantity",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "SoldQuantity",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DiscountCode",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "OrderItems");
        }
    }
}
