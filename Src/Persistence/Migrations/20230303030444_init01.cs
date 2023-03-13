using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Savings_FutureSavings_FutureSavingId",
                table: "Savings");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FutureTransactions_FutureTransactionId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_FutureSavings_FutureSavingId",
                table: "Savings",
                column: "FutureSavingId",
                principalTable: "FutureSavings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FutureTransactions_FutureTransactionId",
                table: "Transactions",
                column: "FutureTransactionId",
                principalTable: "FutureTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
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
    }
}