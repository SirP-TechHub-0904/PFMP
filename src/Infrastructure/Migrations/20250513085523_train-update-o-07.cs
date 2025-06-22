using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class trainupdateo07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_AspNetUsers_FacilitatorId",
                table: "TimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_ModuleTopics_ModuleTopicId",
                table: "TimeTables");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_FacilitatorId",
                table: "TimeTables");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_ModuleTopicId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "FacilitatorId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "ModuleTopicId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "Publish",
                table: "TimeTables");

            migrationBuilder.AddColumn<string>(
                name: "ResourcePerson",
                table: "TimeTables",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourcePerson",
                table: "TimeTables");

            migrationBuilder.AddColumn<string>(
                name: "FacilitatorId",
                table: "TimeTables",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModuleTopicId",
                table: "TimeTables",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Publish",
                table: "TimeTables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_FacilitatorId",
                table: "TimeTables",
                column: "FacilitatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_ModuleTopicId",
                table: "TimeTables",
                column: "ModuleTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_AspNetUsers_FacilitatorId",
                table: "TimeTables",
                column: "FacilitatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_ModuleTopics_ModuleTopicId",
                table: "TimeTables",
                column: "ModuleTopicId",
                principalTable: "ModuleTopics",
                principalColumn: "Id");
        }
    }
}
