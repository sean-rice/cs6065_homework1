using Microsoft.EntityFrameworkCore.Migrations;

namespace Cs6065_Homework1.Data.Migrations
{
    public partial class AddEmailAndFKAttributeToUserInfoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserInfoEntitys",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserInfoEntitys");
        }
    }
}
