using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBCapstoneProject.Migrations
{
    public partial class AddedRegistrationChecking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CompletedRegistration",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditHours",
                table: "CourseForRegistrations");

            migrationBuilder.DropColumn(
                name: "CompletedRegistration",
                table: "AspNetUsers");
        }
    }
}
