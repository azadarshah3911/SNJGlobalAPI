using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SNJGlobalAPI.Migrations
{
    public partial class AgentApprovedStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stages",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Chassing Lead");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Fk_StageId", "Name" },
                values: new object[] { 31, 5, "Agent Approved" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 31);

            migrationBuilder.UpdateData(
                table: "Stages",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Proccesed Lead");
        }
    }
}
