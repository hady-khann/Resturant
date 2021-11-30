using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resturant.Core.Migrations
{
    public partial class ReCorrectionDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResturantInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResturantInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ResturantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResturantInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResturantInfo_Resturant",
                        column: x => x.ResturantID,
                        principalTable: "Resturant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResturantInfo_ResturantID",
                table: "ResturantInfo",
                column: "ResturantID");
        }
    }
}
