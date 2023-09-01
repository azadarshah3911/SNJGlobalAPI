using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SNJGlobalAPI.Migrations
{
    public partial class HalfWayAddedInChassing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Fk_StageId", "Name" },
                values: new object[] { 36, 5, "Can't Process" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Fk_StageId", "Name" },
                values: new object[] { 37, 5, "Chart Notes" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Fk_StageId", "Name" },
                values: new object[] { 38, 1, "Chassing Halfway" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 38);
        }
    }
}
