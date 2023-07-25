using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coba_Net.Migrations
{
    public partial class FlowControl3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedTime",
                table: "Rents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledTime",
                table: "Rents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedTime",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "CancelledTime",
                table: "Rents");
        }
    }
}
