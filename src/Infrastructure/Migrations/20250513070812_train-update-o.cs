using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class trainupdateo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TrainingId",
                table: "EvaluationQuestions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestions_TrainingId",
                table: "EvaluationQuestions",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationQuestions_Trainings_TrainingId",
                table: "EvaluationQuestions",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationQuestions_Trainings_TrainingId",
                table: "EvaluationQuestions");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationQuestions_TrainingId",
                table: "EvaluationQuestions");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "EvaluationQuestions");
        }
    }
}
