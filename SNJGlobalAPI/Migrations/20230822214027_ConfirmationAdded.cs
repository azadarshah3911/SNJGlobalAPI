using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SNJGlobalAPI.Migrations
{
    public partial class ConfirmationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Confirmations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fk_LeadId = table.Column<int>(type: "int", nullable: true),
                    Fk_StatusId = table.Column<int>(type: "int", nullable: true),
                    Fk_UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confirmations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Confirmations_Leads_Fk_LeadId",
                        column: x => x.Fk_LeadId,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Confirmations_Statuses_Fk_StatusId",
                        column: x => x.Fk_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Confirmations_Users_Fk_UserId",
                        column: x => x.Fk_UserId,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Confirmations_Fk_LeadId",
                table: "Confirmations",
                column: "Fk_LeadId",
                unique: true,
                filter: "[Fk_LeadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Confirmations_Fk_StatusId",
                table: "Confirmations",
                column: "Fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Confirmations_Fk_UserId",
                table: "Confirmations",
                column: "Fk_UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Confirmations");
        }
    }
}
