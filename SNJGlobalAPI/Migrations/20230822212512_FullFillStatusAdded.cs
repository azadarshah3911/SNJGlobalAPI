using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SNJGlobalAPI.Migrations
{
    public partial class FullFillStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "ID", "Name", "StageNo" },
                values: new object[] { 8, "Full Fill", 8 });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Fk_StageId", "Name" },
                values: new object[,]
                {
                    { 32, 6, "Done" },
                    { 33, 6, "Denied" },
                    { 34, 1, "Full Fill" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 34);
        }
    }
}
