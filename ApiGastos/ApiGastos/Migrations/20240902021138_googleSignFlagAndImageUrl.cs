using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGastos.Migrations
{
    /// <inheritdoc />
    public partial class googleSignFlagAndImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GoogleSignedIn",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleSignedIn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");
        }
    }
}
