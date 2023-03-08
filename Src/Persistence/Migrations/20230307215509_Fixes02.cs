using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fixes02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FutureTransactions_Id",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FutureTransactionId",
                table: "Transactions",
                column: "FutureTransactionId");

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
                name: "FK_Transactions_FutureTransactions_FutureTransactionId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FutureTransactionId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FutureTransactions_Id",
                table: "Transactions",
                column: "Id",
                principalTable: "FutureTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
