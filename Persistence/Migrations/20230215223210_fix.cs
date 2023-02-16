using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FromAccountId",
                table: "Savings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ToAccountId",
                table: "Savings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Savings_FromAccountId",
                table: "Savings",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Savings_ToAccountId",
                table: "Savings",
                column: "ToAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_Accounts_FromAccountId",
                table: "Savings",
                column: "FromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_Accounts_ToAccountId",
                table: "Savings",
                column: "ToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Savings_Accounts_FromAccountId",
                table: "Savings");

            migrationBuilder.DropForeignKey(
                name: "FK_Savings_Accounts_ToAccountId",
                table: "Savings");

            migrationBuilder.DropIndex(
                name: "IX_Savings_FromAccountId",
                table: "Savings");

            migrationBuilder.DropIndex(
                name: "IX_Savings_ToAccountId",
                table: "Savings");

            migrationBuilder.DropColumn(
                name: "FromAccountId",
                table: "Savings");

            migrationBuilder.DropColumn(
                name: "ToAccountId",
                table: "Savings");
        }
    }
}
