using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intoemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailResponseStatuses_EmailLists_EmailListId",
                table: "EmailResponseStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailSendingStatuses_EmailLists_EmailListId",
                table: "EmailSendingStatuses");

            migrationBuilder.RenameColumn(
                name: "EmailListId",
                table: "EmailSendingStatuses",
                newName: "UserCategoryGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailSendingStatuses_EmailListId",
                table: "EmailSendingStatuses",
                newName: "IX_EmailSendingStatuses_UserCategoryGroupId");

            migrationBuilder.RenameColumn(
                name: "EmailListId",
                table: "EmailResponseStatuses",
                newName: "UserCategoryGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailResponseStatuses_EmailListId",
                table: "EmailResponseStatuses",
                newName: "IX_EmailResponseStatuses_UserCategoryGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailResponseStatuses_UserCategoryGroups_UserCategoryGroupId",
                table: "EmailResponseStatuses",
                column: "UserCategoryGroupId",
                principalTable: "UserCategoryGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSendingStatuses_UserCategoryGroups_UserCategoryGroupId",
                table: "EmailSendingStatuses",
                column: "UserCategoryGroupId",
                principalTable: "UserCategoryGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailResponseStatuses_UserCategoryGroups_UserCategoryGroupId",
                table: "EmailResponseStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailSendingStatuses_UserCategoryGroups_UserCategoryGroupId",
                table: "EmailSendingStatuses");

            migrationBuilder.RenameColumn(
                name: "UserCategoryGroupId",
                table: "EmailSendingStatuses",
                newName: "EmailListId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailSendingStatuses_UserCategoryGroupId",
                table: "EmailSendingStatuses",
                newName: "IX_EmailSendingStatuses_EmailListId");

            migrationBuilder.RenameColumn(
                name: "UserCategoryGroupId",
                table: "EmailResponseStatuses",
                newName: "EmailListId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailResponseStatuses_UserCategoryGroupId",
                table: "EmailResponseStatuses",
                newName: "IX_EmailResponseStatuses_EmailListId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailResponseStatuses_EmailLists_EmailListId",
                table: "EmailResponseStatuses",
                column: "EmailListId",
                principalTable: "EmailLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSendingStatuses_EmailLists_EmailListId",
                table: "EmailSendingStatuses",
                column: "EmailListId",
                principalTable: "EmailLists",
                principalColumn: "Id");
        }
    }
}
