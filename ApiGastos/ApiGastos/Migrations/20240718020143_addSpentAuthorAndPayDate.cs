using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGastos.Migrations
{
    /// <inheritdoc />
    public partial class addSpentAuthorAndPayDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Spent",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PayedAt",
                table: "Spent",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Spent_AuthorId",
                table: "Spent",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spent_AspNetUsers_AuthorId",
                table: "Spent",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spent_AspNetUsers_AuthorId",
                table: "Spent");

            migrationBuilder.DropIndex(
                name: "IX_Spent_AuthorId",
                table: "Spent");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Spent");

            migrationBuilder.DropColumn(
                name: "PayedAt",
                table: "Spent");
        }
    }
}
