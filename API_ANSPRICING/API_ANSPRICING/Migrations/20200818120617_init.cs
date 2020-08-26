using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_ANSPRICING.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    stationID = table.Column<string>(nullable: true),
                    shopCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    StationId = table.Column<Guid>(nullable: false),
                    tagId = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    coutry = table.Column<string>(nullable: true),
                    manufacturer = table.Column<string>(nullable: true),
                    oldPrice = table.Column<string>(nullable: true),
                    price = table.Column<string>(nullable: true),
                    description1 = table.Column<string>(nullable: true),
                    description2 = table.Column<string>(nullable: true),
                    description3 = table.Column<string>(nullable: true),
                    description4 = table.Column<string>(nullable: true),
                    description5 = table.Column<string>(nullable: true),
                    description6 = table.Column<string>(nullable: true),
                    imgSource = table.Column<string>(nullable: true),
                    QrCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_stations_StationId",
                        column: x => x.StationId,
                        principalTable: "stations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tags_StationId",
                table: "tags",
                column: "StationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "stations");
        }
    }
}
