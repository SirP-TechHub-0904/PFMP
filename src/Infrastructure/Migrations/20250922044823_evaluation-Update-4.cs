using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class evaluationUpdate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DialyActivityId",
                table: "DialyEvaluationQuestions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DialyEvaluationQuestions_DialyActivityId",
                table: "DialyEvaluationQuestions",
                column: "DialyActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyEvaluationQuestions_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestions",
                column: "DialyActivityId",
                principalTable: "DialyActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyEvaluationQuestions_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.DropIndex(
                name: "IX_DialyEvaluationQuestions_DialyActivityId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.DropColumn(
                name: "DialyActivityId",
                table: "DialyEvaluationQuestions");
        }
    }
}
