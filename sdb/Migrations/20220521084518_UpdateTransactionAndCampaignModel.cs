using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sdb.Migrations
{
    public partial class UpdateTransactionAndCampaignModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex("IX_sdb_transaction_donorId", "sdb_transaction", "donorId");
            migrationBuilder.CreateIndex("IX_sdb_transaction_compaignId", "sdb_transaction", "compaignId");
            migrationBuilder.DropForeignKey(
                name: "FK_sdb_transaction_sdb_compaigns",
                table: "sdb_transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_sdb_transaction_sdb_system_users",
                table: "sdb_transaction");

            migrationBuilder.DropColumn(
                name: "cardNumber",
                table: "sdb_transaction");

            migrationBuilder.DropColumn(
                name: "csv",
                table: "sdb_transaction");

            migrationBuilder.DropColumn(
                name: "expiryDate",
                table: "sdb_transaction");

            migrationBuilder.AlterColumn<int>(
                name: "donorId",
                table: "sdb_transaction",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "compaignId",
                table: "sdb_transaction",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "collectedAmount",
                table: "sdb_compaigns",
                type: "numeric(18, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18, 0)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_transaction_sdb_compaigns",
                table: "sdb_transaction",
                column: "compaignId",
                principalTable: "sdb_compaigns",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_transaction_sdb_system_users",
                table: "sdb_transaction",
                column: "donorId",
                principalTable: "sdb_system_users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sdb_transaction_sdb_compaigns",
                table: "sdb_transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_sdb_transaction_sdb_system_users",
                table: "sdb_transaction");

            migrationBuilder.AlterColumn<int>(
                name: "donorId",
                table: "sdb_transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "compaignId",
                table: "sdb_transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "cardNumber",
                table: "sdb_transaction",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "csv",
                table: "sdb_transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "expiryDate",
                table: "sdb_transaction",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "collectedAmount",
                table: "sdb_compaigns",
                type: "numeric(18, 0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18, 0)");

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_transaction_sdb_compaigns",
                table: "sdb_transaction",
                column: "compaignId",
                principalTable: "sdb_compaigns",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_transaction_sdb_system_users",
                table: "sdb_transaction",
                column: "donorId",
                principalTable: "sdb_system_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
