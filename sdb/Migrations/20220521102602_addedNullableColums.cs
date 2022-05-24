using Microsoft.EntityFrameworkCore.Migrations;

namespace sdb.Migrations
{
    public partial class addedNullableColums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sdb_compaigns_sdb_system_users_sdbSystemUsersId",
                table: "sdb_compaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_sdb_transaction_sdb_compaigns",
                table: "sdb_transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_sdb_transaction_sdb_system_users",
                table: "sdb_transaction");

            migrationBuilder.AlterColumn<int>(
                name: "donorId",
                table: "sdb_transaction",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "compaignId",
                table: "sdb_transaction",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "sdbSystemUsersId",
                table: "sdb_compaigns",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_compaigns_sdb_system_users_sdbSystemUsersId",
                table: "sdb_compaigns",
                column: "sdbSystemUsersId",
                principalTable: "sdb_system_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sdb_compaigns_sdb_system_users_sdbSystemUsersId",
                table: "sdb_compaigns");

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
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "compaignId",
                table: "sdb_transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "sdbSystemUsersId",
                table: "sdb_compaigns",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_compaigns_sdb_system_users_sdbSystemUsersId",
                table: "sdb_compaigns",
                column: "sdbSystemUsersId",
                principalTable: "sdb_system_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_transaction_sdb_compaigns",
                table: "sdb_transaction",
                column: "compaignId",
                principalTable: "sdb_compaigns",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_transaction_sdb_system_users",
                table: "sdb_transaction",
                column: "donorId",
                principalTable: "sdb_system_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
