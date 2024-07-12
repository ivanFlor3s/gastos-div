using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGastos.Migrations
{
    /// <inheritdoc />
    public partial class spentMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserSpent_Spent_SpentSpendId",
                table: "GroupUserSpent");

            migrationBuilder.RenameColumn(
                name: "SpendId",
                table: "Spent",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SpentSpendId",
                table: "GroupUserSpent",
                newName: "SpentId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUserSpent_SpentSpendId",
                table: "GroupUserSpent",
                newName: "IX_GroupUserSpent_SpentId");

            migrationBuilder.AddColumn<int>(
                name: "SpentMode",
                table: "Spent",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "nuevo",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserSpent_Spent_SpentId",
                table: "GroupUserSpent",
                column: "SpentId",
                principalTable: "Spent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserSpent_Spent_SpentId",
                table: "GroupUserSpent");

            migrationBuilder.DropColumn(
                name: "SpentMode",
                table: "Spent");

            migrationBuilder.DropColumn(
                name: "nuevo",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Spent",
                newName: "SpendId");

            migrationBuilder.RenameColumn(
                name: "SpentId",
                table: "GroupUserSpent",
                newName: "SpentSpendId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUserSpent_SpentId",
                table: "GroupUserSpent",
                newName: "IX_GroupUserSpent_SpentSpendId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserSpent_Spent_SpentSpendId",
                table: "GroupUserSpent",
                column: "SpentSpendId",
                principalTable: "Spent",
                principalColumn: "SpendId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
