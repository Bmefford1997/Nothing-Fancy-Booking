using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nothing_Fancy.Migrations.ReservationDb
{
    public partial class addedBeginAndEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reserveDate",
                table: "Reservation");

            migrationBuilder.AddColumn<DateTime>(
                name: "reserveDateBegin",
                table: "Reservation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "reserveDateEnd",
                table: "Reservation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reserveDateBegin",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "reserveDateEnd",
                table: "Reservation");

            migrationBuilder.AddColumn<DateTime>(
                name: "reserveDate",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
