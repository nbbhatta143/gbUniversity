using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBCapstoneProject.Migrations
{
    public partial class LinkUsersToRolesInSchool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserID",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserID",
                table: "Faculty",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUserID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AspNetUserID",
                table: "Faculty");

            migrationBuilder.AddColumn<int>(
                name: "RoleInSchoolID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
