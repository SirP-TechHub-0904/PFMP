using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class usergroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCategoryGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ColumnOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnFour = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategoryGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCategoryGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCategoryGroups_UserCategories_UserCategoryId",
                        column: x => x.UserCategoryId,
                        principalTable: "UserCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCategoryGroups_UserCategoryId",
                table: "UserCategoryGroups",
                column: "UserCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategoryGroups_UserId",
                table: "UserCategoryGroups",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCategoryGroups");

            migrationBuilder.DropTable(
                name: "UserCategories");
        }
    }
}
