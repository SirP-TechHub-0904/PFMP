using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class serviceemail00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailGroups_AspNetUsers_AppUserId",
                table: "EmailGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailLists_AspNetUsers_AppUserId",
                table: "EmailLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailSendingStatuses_AspNetUsers_UserId",
                table: "EmailSendingStatuses");

            migrationBuilder.DropIndex(
                name: "IX_EmailSendingStatuses_UserId",
                table: "EmailSendingStatuses");

            migrationBuilder.DropIndex(
                name: "IX_EmailLists_AppUserId",
                table: "EmailLists");

            migrationBuilder.DropIndex(
                name: "IX_EmailGroups_AppUserId",
                table: "EmailGroups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmailSendingStatuses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "EmailLists");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "EmailGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EmailSendingStatuses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "EmailLists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "EmailGroups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailSendingStatuses_UserId",
                table: "EmailSendingStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLists_AppUserId",
                table: "EmailLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailGroups_AppUserId",
                table: "EmailGroups",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailGroups_AspNetUsers_AppUserId",
                table: "EmailGroups",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailLists_AspNetUsers_AppUserId",
                table: "EmailLists",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSendingStatuses_AspNetUsers_UserId",
                table: "EmailSendingStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
