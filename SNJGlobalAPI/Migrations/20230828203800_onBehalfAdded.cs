using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SNJGlobalAPI.Migrations
{
    public partial class onBehalfAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OnBehalf",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnBehalf",
                table: "Leads");
        }
    }
}
