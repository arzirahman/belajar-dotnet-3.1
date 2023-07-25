using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coba_Net.Migrations
{
    public partial class FlowControl4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rents");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Rents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Rents");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
