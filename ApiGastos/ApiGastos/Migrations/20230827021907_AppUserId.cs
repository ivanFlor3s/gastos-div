using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGastos.Migrations
{
    /// <inheritdoc />
    public partial class AppUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_AspNetUsers_AppUserId",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Spent_GroupUsers_GroupUserGroupId_GroupUserUserId",
                table: "Spent");

            migrationBuilder.DropIndex(
                name: "IX_Spent_GroupUserGroupId_GroupUserUserId",
                table: "Spent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "GroupUserUserId",
                table: "Spent");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GroupUsers");

            migrationBuilder.AddColumn<string>(
                name: "GroupUserAppUserId",
                table: "Spent",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "GroupUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                columns: new[] { "GroupId", "AppUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Spent_GroupUserGroupId_GroupUserAppUserId",
                table: "Spent",
                columns: new[] { "GroupUserGroupId", "GroupUserAppUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_AspNetUsers_AppUserId",
                table: "GroupUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spent_GroupUsers_GroupUserGroupId_GroupUserAppUserId",
                table: "Spent",
                columns: new[] { "GroupUserGroupId", "GroupUserAppUserId" },
                principalTable: "GroupUsers",
                principalColumns: new[] { "GroupId", "AppUserId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_AspNetUsers_AppUserId",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Spent_GroupUsers_GroupUserGroupId_GroupUserAppUserId",
                table: "Spent");

            migrationBuilder.DropIndex(
                name: "IX_Spent_GroupUserGroupId_GroupUserAppUserId",
                table: "Spent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "GroupUserAppUserId",
                table: "Spent");

            migrationBuilder.AddColumn<int>(
                name: "GroupUserUserId",
                table: "Spent",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "GroupUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GroupUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Spent_GroupUserGroupId_GroupUserUserId",
                table: "Spent",
                columns: new[] { "GroupUserGroupId", "GroupUserUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_AspNetUsers_AppUserId",
                table: "GroupUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Spent_GroupUsers_GroupUserGroupId_GroupUserUserId",
                table: "Spent",
                columns: new[] { "GroupUserGroupId", "GroupUserUserId" },
                principalTable: "GroupUsers",
                principalColumns: new[] { "GroupId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
