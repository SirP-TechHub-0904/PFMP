using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entityflow3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_EvaluationQuestions_EvaluationQuestionId",
                table: "DialyUserEvaluations");

            migrationBuilder.AlterColumn<long>(
                name: "EvaluationQuestionId",
                table: "DialyUserEvaluations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "DialyUserEvaluations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_EvaluationQuestions_EvaluationQuestionId",
                table: "DialyUserEvaluations",
                column: "EvaluationQuestionId",
                principalTable: "EvaluationQuestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_EvaluationQuestions_EvaluationQuestionId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "DialyUserEvaluations");

            migrationBuilder.AlterColumn<long>(
                name: "EvaluationQuestionId",
                table: "DialyUserEvaluations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_EvaluationQuestions_EvaluationQuestionId",
                table: "DialyUserEvaluations",
                column: "EvaluationQuestionId",
                principalTable: "EvaluationQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
