using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SNJGlobalAPI.Migrations
{
    public partial class DeletedByAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Leads",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FK_DeletedBy",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_FK_DeletedBy",
                table: "Leads",
                column: "FK_DeletedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Users_FK_DeletedBy",
                table: "Leads",
                column: "FK_DeletedBy",
                principalTable: "Users",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Users_FK_DeletedBy",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Leads_FK_DeletedBy",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "FK_DeletedBy",
                table: "Leads");
        }
    }
}
