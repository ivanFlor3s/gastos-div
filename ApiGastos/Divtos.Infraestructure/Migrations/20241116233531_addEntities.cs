using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Divtos.Infraestructure.Migrations
{
    public partial class addEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "character varying(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<string>(type: "character varying(36)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedById = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(36)", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PayedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SpentMode = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<string>(type: "character varying(36)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedById = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spents_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spents_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spents_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spents_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpentUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    SpentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpentUsers", x => new { x.SpentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SpentUsers_Spents_SpentId",
                        column: x => x.SpentId,
                        principalTable: "Spents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpentUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatorId",
                table: "Groups",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LastModifiedById",
                table: "Groups",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserId",
                table: "GroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Spents_AuthorId",
                table: "Spents",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Spents_CreatorId",
                table: "Spents",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Spents_GroupId",
                table: "Spents",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Spents_LastModifiedById",
                table: "Spents",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SpentUsers_UserId",
                table: "SpentUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "SpentUsers");

            migrationBuilder.DropTable(
                name: "Spents");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(36)");
        }
    }
}
