using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatetest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "TrainingTests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTests_CategoryId",
                table: "TrainingTests",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTests_TrainingCategories_CategoryId",
                table: "TrainingTests",
                column: "CategoryId",
                principalTable: "TrainingCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTests_TrainingCategories_CategoryId",
                table: "TrainingTests");

            migrationBuilder.DropIndex(
                name: "IX_TrainingTests_CategoryId",
                table: "TrainingTests");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TrainingTests");
        }
    }
}
