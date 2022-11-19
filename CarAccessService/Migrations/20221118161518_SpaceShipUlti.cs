using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAccessService.Migrations
{
    public partial class SpaceShipUlti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpaceShipModels",
                columns: table => new
                {
                    SpaceShipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteringsNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParkingLotNumber = table.Column<int>(type: "int", nullable: true),
                    EnterTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceShipModels", x => x.SpaceShipID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingLotModels",
                columns: table => new
                {
                    SpaceParkingLotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parkingLotLevel = table.Column<int>(type: "int", nullable: false),
                    parkingLotNumber = table.Column<int>(type: "int", nullable: false),
                    SpaceShipID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingLotModels", x => x.SpaceParkingLotId);
                    table.ForeignKey(
                        name: "FK_ParkingLotModels_SpaceShipModels_SpaceShipID",
                        column: x => x.SpaceShipID,
                        principalTable: "SpaceShipModels",
                        principalColumn: "SpaceShipID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLotModels_SpaceShipID",
                table: "ParkingLotModels",
                column: "SpaceShipID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingLotModels");

            migrationBuilder.DropTable(
                name: "SpaceShipModels");
        }
    }
}
