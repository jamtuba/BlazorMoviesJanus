using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorMovies.Server.Migrations
{
    public partial class AdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               INSERT INTO AspNetRoles (Id, [Name], NormalizedName)
               VALUES('f52c2cb3-8d0b-4f91-ad7f-34dc983810e3', 'Admin', 'Admin') 
               ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
