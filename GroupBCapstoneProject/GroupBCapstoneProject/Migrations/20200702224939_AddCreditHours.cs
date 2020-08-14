using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBCapstoneProject.Migrations
{
    public partial class AddCreditHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditHours",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseForRegistrations");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CreditHours",
                table: "Courses");
        }
    }
}
