using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGastos.Migrations
{
    /// <inheritdoc />
    public partial class fixAppUserSpentLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserSpent_AspNetUsers_AppUserId",
                table: "GroupUserSpent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUserSpent",
                table: "GroupUserSpent");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GroupUserSpent");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "GroupUserSpent",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUserSpent",
                table: "GroupUserSpent",
                columns: new[] { "GroupId", "AppUserId", "SpentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserSpent_AspNetUsers_AppUserId",
                table: "GroupUserSpent",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserSpent_AspNetUsers_AppUserId",
                table: "GroupUserSpent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUserSpent",
                table: "GroupUserSpent");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "GroupUserSpent",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GroupUserSpent",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUserSpent",
                table: "GroupUserSpent",
                columns: new[] { "GroupId", "UserId", "SpentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserSpent_AspNetUsers_AppUserId",
                table: "GroupUserSpent",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
