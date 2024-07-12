using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGastos.Migrations
{
    /// <inheritdoc />
    public partial class SpentUpOneLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spent_GroupUsers_GroupUserGroupId_GroupUserAppUserId",
                table: "Spent");

            migrationBuilder.DropTable(
                name: "GroupUserSpent");

            migrationBuilder.DropIndex(
                name: "IX_Spent_GroupUserGroupId_GroupUserAppUserId",
                table: "Spent");

            migrationBuilder.DropColumn(
                name: "GroupUserAppUserId",
                table: "Spent");

            migrationBuilder.DropColumn(
                name: "nuevo",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "GroupUserGroupId",
                table: "Spent",
                newName: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Spent_GroupId",
                table: "Spent",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spent_Groups_GroupId",
                table: "Spent",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spent_Groups_GroupId",
                table: "Spent");

            migrationBuilder.DropIndex(
                name: "IX_Spent_GroupId",
                table: "Spent");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Spent",
                newName: "GroupUserGroupId");

            migrationBuilder.AddColumn<string>(
                name: "GroupUserAppUserId",
                table: "Spent",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nuevo",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GroupUserSpent",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<string>(type: "text", nullable: false),
                    SpentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserSpent", x => new { x.GroupId, x.AppUserId, x.SpentId });
                    table.ForeignKey(
                        name: "FK_GroupUserSpent_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserSpent_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserSpent_Spent_SpentId",
                        column: x => x.SpentId,
                        principalTable: "Spent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spent_GroupUserGroupId_GroupUserAppUserId",
                table: "Spent",
                columns: new[] { "GroupUserGroupId", "GroupUserAppUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserSpent_AppUserId",
                table: "GroupUserSpent",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserSpent_SpentId",
                table: "GroupUserSpent",
                column: "SpentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spent_GroupUsers_GroupUserGroupId_GroupUserAppUserId",
                table: "Spent",
                columns: new[] { "GroupUserGroupId", "GroupUserAppUserId" },
                principalTable: "GroupUsers",
                principalColumns: new[] { "GroupId", "AppUserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
