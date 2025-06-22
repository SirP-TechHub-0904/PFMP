using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class serviceemail0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_AspNetUsers_UserId",
                table: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_Servers_UserId",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Servers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Servers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servers_UserId",
                table: "Servers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_AspNetUsers_UserId",
                table: "Servers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
