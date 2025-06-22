using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class trainupdate19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyEvaluationQuestions_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_DialyActivities_DialyActivityId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_DialyUserEvaluations_DialyActivityId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_DialyEvaluationQuestions_DialyActivityId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.DropColumn(
                name: "DialyActivityId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropColumn(
                name: "DialyActivityId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.AddColumn<long>(
                name: "TrainingId",
                table: "DialyUserEvaluations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TrainingId",
                table: "DialyEvaluationQuestions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DialyUserEvaluations_TrainingId",
                table: "DialyUserEvaluations",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_DialyEvaluationQuestions_TrainingId",
                table: "DialyEvaluationQuestions",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyEvaluationQuestions_Trainings_TrainingId",
                table: "DialyEvaluationQuestions",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_Trainings_TrainingId",
                table: "DialyUserEvaluations",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyEvaluationQuestions_Trainings_TrainingId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_Trainings_TrainingId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_DialyUserEvaluations_TrainingId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_DialyEvaluationQuestions_TrainingId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.AddColumn<long>(
                name: "DialyActivityId",
                table: "DialyUserEvaluations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DialyActivityId",
                table: "DialyEvaluationQuestions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DialyUserEvaluations_DialyActivityId",
                table: "DialyUserEvaluations",
                column: "DialyActivityId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_DialyActivities_DialyActivityId",
                table: "DialyUserEvaluations",
                column: "DialyActivityId",
                principalTable: "DialyActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
