using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class DDL__CreateVoyage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voyages",
                columns: table => new
                {
                    IdVoyage = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoyageDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdShip = table.Column<int>(type: "integer", nullable: false),
                    DeparturePortId = table.Column<int>(type: "integer", nullable: false),
                    ArrivalPortId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voyages", x => x.IdVoyage);
                    table.ForeignKey(
                        name: "FK_Voyages_Ports_ArrivalPortId",
                        column: x => x.ArrivalPortId,
                        principalTable: "Ports",
                        principalColumn: "IdPort",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voyages_Ports_DeparturePortId",
                        column: x => x.DeparturePortId,
                        principalTable: "Ports",
                        principalColumn: "IdPort",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voyages_Ships_IdShip",
                        column: x => x.IdShip,
                        principalTable: "Ships",
                        principalColumn: "IdShip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voyages_ArrivalPortId",
                table: "Voyages",
                column: "ArrivalPortId");

            migrationBuilder.CreateIndex(
                name: "IX_Voyages_DeparturePortId",
                table: "Voyages",
                column: "DeparturePortId");

            migrationBuilder.CreateIndex(
                name: "IX_Voyages_IdShip",
                table: "Voyages",
                column: "IdShip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voyages");
        }
    }
}
