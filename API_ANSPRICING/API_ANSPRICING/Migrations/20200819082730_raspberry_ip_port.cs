using Microsoft.EntityFrameworkCore.Migrations;

namespace API_ANSPRICING.Migrations
{
    public partial class raspberry_ip_port : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "stations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PORT",
                table: "stations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IP",
                table: "stations");

            migrationBuilder.DropColumn(
                name: "PORT",
                table: "stations");
        }
    }
}
