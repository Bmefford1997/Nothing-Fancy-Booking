using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nothing_Fancy.Migrations.ReservationDb
{
    public partial class addReservationContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reserverName = table.Column<string>(nullable: false),
                    nameOfRoom = table.Column<string>(nullable: true),
                    reserveDate = table.Column<DateTime>(nullable: false),
                    cost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
