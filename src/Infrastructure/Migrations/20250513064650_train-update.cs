using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class trainupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTests_TrainingCategories_CategoryId",
                table: "TrainingTests");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "TrainingTests",
                newName: "TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingTests_CategoryId",
                table: "TrainingTests",
                newName: "IX_TrainingTests_TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTests_Trainings_TrainingId",
                table: "TrainingTests",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTests_Trainings_TrainingId",
                table: "TrainingTests");

            migrationBuilder.RenameColumn(
                name: "TrainingId",
                table: "TrainingTests",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingTests_TrainingId",
                table: "TrainingTests",
                newName: "IX_TrainingTests_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTests_TrainingCategories_CategoryId",
                table: "TrainingTests",
                column: "CategoryId",
                principalTable: "TrainingCategories",
                principalColumn: "Id");
        }
    }
}
