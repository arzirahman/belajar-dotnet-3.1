using Microsoft.EntityFrameworkCore.Migrations;

namespace Coba_Net.Migrations
{
    public partial class FlowControl6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelledByAdmin",
                table: "Rents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelledByAdmin",
                table: "Rents");
        }
    }
}
