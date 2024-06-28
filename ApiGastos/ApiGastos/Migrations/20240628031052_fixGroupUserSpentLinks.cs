using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGastos.Migrations
{
    /// <inheritdoc />
    public partial class fixGroupUserSpentLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUserSpent",
                table: "GroupUserSpent");

            migrationBuilder.DropColumn(
                name: "SpendId",
                table: "GroupUserSpent");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GroupUserSpent",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUserSpent",
                table: "GroupUserSpent",
                columns: new[] { "GroupId", "UserId", "SpentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUserSpent",
                table: "GroupUserSpent");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "GroupUserSpent",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "SpendId",
                table: "GroupUserSpent",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUserSpent",
                table: "GroupUserSpent",
                columns: new[] { "GroupId", "UserId", "SpendId" });
        }
    }
}
