using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGastos.Migrations
{
    /// <inheritdoc />
    public partial class groupusersoftdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "GroupUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "GroupUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GroupUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GroupUsers");
        }
    }
}
