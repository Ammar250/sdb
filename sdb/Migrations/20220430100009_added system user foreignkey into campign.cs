using Microsoft.EntityFrameworkCore.Migrations;

namespace sdb.Migrations
{
    public partial class addedsystemuserforeignkeyintocampign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sdbSystemUsersId",
                table: "sdb_compaigns",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sdb_compaigns_sdbSystemUsersId",
                table: "sdb_compaigns",
                column: "sdbSystemUsersId");

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

            migrationBuilder.DropIndex(
                name: "IX_sdb_compaigns_sdbSystemUsersId",
                table: "sdb_compaigns");

            migrationBuilder.DropColumn(
                name: "sdbSystemUsersId",
                table: "sdb_compaigns");
        }
    }
}
