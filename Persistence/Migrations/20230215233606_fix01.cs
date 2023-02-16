using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fix01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AspNetUsers_UserId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Budgets");

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BudgetId",
                table: "AspNetUsers",
                column: "BudgetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Budgets_BudgetId",
                table: "AspNetUsers",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Budgets_BudgetId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BudgetId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Budgets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AspNetUsers_UserId",
                table: "Budgets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
