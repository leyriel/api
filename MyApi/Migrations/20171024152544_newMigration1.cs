using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyApi.Migrations
{
    public partial class newMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Users_Establishments",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "EstablishmentID",
                table: "Users",
                column: "EstablishmentID",
                principalTable: "Establishments",
                principalColumn: "EstablishmentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "EstablishmentID",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Users_Establishments",
                table: "Users",
                column: "EstablishmentID",
                principalTable: "Establishments",
                principalColumn: "EstablishmentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
