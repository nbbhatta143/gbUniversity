using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBCapstoneProject.Migrations
{
    public partial class AddRoleInSchoolID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleInSchoolID",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropColumn(
                name: "RoleInSchoolID",
                table: "AspNetUsers");
        }
    }
}
