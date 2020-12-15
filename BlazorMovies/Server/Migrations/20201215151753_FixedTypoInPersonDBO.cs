using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BlazorMovies.Server.Migrations
{
    public partial class FixedTypoInPersonDBO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatoOfBirth",
                table: "People");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "People",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "People");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatoOfBirth",
                table: "People",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
