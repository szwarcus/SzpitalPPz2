using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Repository.Migrations
{
    public partial class AppUserChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SystemRoleName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemRoleName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
