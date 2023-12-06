using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uprise.Migrations
{
    public partial class InitialMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "plants");

            migrationBuilder.CreateTable(
                name: "power_plants",
                schema: "plants",
                columns: table => new
                {
                    id = table.Column<int>(type: "serial", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    installed_power = table.Column<string>(type: "character varying", nullable: false),
                    date_of_installation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    latitude = table.Column<float>(type: "real", nullable: false),
                    longitude = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_power_plants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "forecasted_productions",
                schema: "plants",
                columns: table => new
                {
                    power_plant_id = table.Column<int>(type: "integer", nullable: false),
                    power = table.Column<double>(type: "double precision", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_forecasted_productions_power_plants_power_plant_id",
                        column: x => x.power_plant_id,
                        principalSchema: "plants",
                        principalTable: "power_plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "real_productions",
                schema: "plants",
                columns: table => new
                {
                    power_plant_id = table.Column<int>(type: "integer", nullable: false),
                    power = table.Column<double>(type: "double precision", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_real_productions_power_plants_power_plant_id",
                        column: x => x.power_plant_id,
                        principalSchema: "plants",
                        principalTable: "power_plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_forecasted_productions_power_plant_id",
                schema: "plants",
                table: "forecasted_productions",
                column: "power_plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_real_productions_power_plant_id",
                schema: "plants",
                table: "real_productions",
                column: "power_plant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "forecasted_productions",
                schema: "plants");

            migrationBuilder.DropTable(
                name: "real_productions",
                schema: "plants");

            migrationBuilder.DropTable(
                name: "power_plants",
                schema: "plants");
        }
    }
}
