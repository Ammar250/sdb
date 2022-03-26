using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sdb.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sdb_compaign_purpose");

            migrationBuilder.DropTable(
                name: "sdb_payments");

            migrationBuilder.DropTable(
                name: "sdb_roles");

            migrationBuilder.DropTable(
                name: "sdb_transaction");

            migrationBuilder.DropTable(
                name: "sdb_compaigns");

            migrationBuilder.DropTable(
                name: "sdb_system_users");
        }
    }
}
