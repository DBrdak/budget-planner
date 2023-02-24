using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesRelationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FutureSavings_Accounts_FromAccountId",
                table: "FutureSavings");

            migrationBuilder.DropForeignKey(
                name: "FK_FutureSavings_Accounts_ToAccountId",
                table: "FutureSavings");

            migrationBuilder.DropForeignKey(
                name: "FK_FutureTransactions_Accounts_AccountId",
                table: "FutureTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "FutureTransactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ToAccountId",
                table: "FutureSavings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FromAccountId",
                table: "FutureSavings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FutureSavings_Accounts_FromAccountId",
                table: "FutureSavings",
                column: "FromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FutureSavings_Accounts_ToAccountId",
                table: "FutureSavings",
                column: "ToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FutureTransactions_Accounts_AccountId",
                table: "FutureTransactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FutureSavings_Accounts_FromAccountId",
                table: "FutureSavings");

            migrationBuilder.DropForeignKey(
                name: "FK_FutureSavings_Accounts_ToAccountId",
                table: "FutureSavings");

            migrationBuilder.DropForeignKey(
                name: "FK_FutureTransactions_Accounts_AccountId",
                table: "FutureTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "FutureTransactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToAccountId",
                table: "FutureSavings",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "FromAccountId",
                table: "FutureSavings",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_FutureSavings_Accounts_FromAccountId",
                table: "FutureSavings",
                column: "FromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FutureSavings_Accounts_ToAccountId",
                table: "FutureSavings",
                column: "ToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FutureTransactions_Accounts_AccountId",
                table: "FutureTransactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
