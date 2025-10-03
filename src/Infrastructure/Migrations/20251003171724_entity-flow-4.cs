using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entityflow4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_EvaluationQuestions_EvaluationQuestionId",
                table: "DialyUserEvaluations");

            migrationBuilder.RenameColumn(
                name: "EvaluationQuestionId",
                table: "DialyUserEvaluations",
                newName: "DialyEvaluationQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_DialyUserEvaluations_EvaluationQuestionId",
                table: "DialyUserEvaluations",
                newName: "IX_DialyUserEvaluations_DialyEvaluationQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_DialyEvaluationQuestions_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations",
                column: "DialyEvaluationQuestionId",
                principalTable: "DialyEvaluationQuestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_DialyEvaluationQuestions_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations");

            migrationBuilder.RenameColumn(
                name: "DialyEvaluationQuestionId",
                table: "DialyUserEvaluations",
                newName: "EvaluationQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_DialyUserEvaluations_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations",
                newName: "IX_DialyUserEvaluations_EvaluationQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_EvaluationQuestions_EvaluationQuestionId",
                table: "DialyUserEvaluations",
                column: "EvaluationQuestionId",
                principalTable: "EvaluationQuestions",
                principalColumn: "Id");
        }
    }
}
