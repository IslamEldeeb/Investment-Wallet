using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinarInvestments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InvestmentOpportunities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 7, 14, 22, 17, 10, 227, DateTimeKind.Utc).AddTicks(1710));

            migrationBuilder.UpdateData(
                table: "InvestmentOpportunities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 7, 14, 22, 17, 10, 227, DateTimeKind.Utc).AddTicks(2460));

            migrationBuilder.UpdateData(
                table: "InvestmentOpportunities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 7, 14, 22, 17, 10, 227, DateTimeKind.Utc).AddTicks(2470));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromWalletId",
                table: "Transactions",
                column: "FromWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToWalletId",
                table: "Transactions",
                column: "ToWalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallet_FromWalletId",
                table: "Transactions",
                column: "FromWalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallet_ToWalletId",
                table: "Transactions",
                column: "ToWalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallet_FromWalletId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallet_ToWalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FromWalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ToWalletId",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "InvestmentOpportunities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 7, 14, 22, 13, 54, 422, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "InvestmentOpportunities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 7, 14, 22, 13, 54, 422, DateTimeKind.Utc).AddTicks(4990));

            migrationBuilder.UpdateData(
                table: "InvestmentOpportunities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 7, 14, 22, 13, 54, 422, DateTimeKind.Utc).AddTicks(5000));
        }
    }
}
