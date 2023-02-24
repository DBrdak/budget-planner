using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesPropsExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FutureTransactionId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FutureSavingId",
                table: "Savings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "CompletedAmount",
                table: "FutureTransactions",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CompletedAmount",
                table: "FutureSavings",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FutureTransactionId",
                table: "Transactions",
                column: "FutureTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Savings_FutureSavingId",
                table: "Savings",
                column: "FutureSavingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_FutureSavings_FutureSavingId",
                table: "Savings",
                column: "FutureSavingId",
                principalTable: "FutureSavings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FutureTransactions_FutureTransactionId",
                table: "Transactions",
                column: "FutureTransactionId",
                principalTable: "FutureTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Savings_FutureSavings_FutureSavingId",
                table: "Savings");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FutureTransactions_FutureTransactionId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FutureTransactionId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Savings_FutureSavingId",
                table: "Savings");

            migrationBuilder.DropColumn(
                name: "FutureTransactionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FutureSavingId",
                table: "Savings");

            migrationBuilder.DropColumn(
                name: "CompletedAmount",
                table: "FutureTransactions");

            migrationBuilder.DropColumn(
                name: "CompletedAmount",
                table: "FutureSavings");
        }
    }
}
