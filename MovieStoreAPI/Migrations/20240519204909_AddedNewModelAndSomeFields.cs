using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStoreAPI.Migrations
{
    public partial class AddedNewModelAndSomeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActorApplication",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Invitation",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActorApplication",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Invitation",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Movie");
        }
    }
}
