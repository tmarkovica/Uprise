using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uprise.Migrations
{
    public partial class InitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "forecasted_productions",
                schema: "plants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_forecasted_productions_power_plant_id",
                schema: "plants",
                table: "forecasted_productions",
                column: "power_plant_id");
        }
    }
}
