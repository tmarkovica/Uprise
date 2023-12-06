using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uprise.Migrations.UpriseDb
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "uprise");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "uprise",
                columns: table => new
                {
                    id = table.Column<int>(type: "serial", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: false),
                    password = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "email_unique",
                schema: "uprise",
                table: "users",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users",
                schema: "uprise");
        }
    }
}
