using Microsoft.EntityFrameworkCore.Migrations;

namespace sdb.Migrations
{
    public partial class updateuserandcompaignrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sdb_compaigns_sdb_system_users_sdbSystemUsersId",
                table: "sdb_compaigns");

            migrationBuilder.AlterColumn<int>(
                name: "sdbSystemUsersId",
                table: "sdb_compaigns",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "sdb_compaigns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_sdb_compaigns_sdb_system_users_sdbSystemUsersId",
                table: "sdb_compaigns",
                column: "sdbSystemUsersId",
                principalTable: "sdb_system_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sdb_compaigns_sdb_system_users_sdbSystemUsersId",
                table: "sdb_compaigns");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "sdb_compaigns");

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
        }
    }
}
