using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class evaluationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_Trainings_TrainingId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_DialyUserEvaluations_TrainingId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "DialyUserEvaluations");

            migrationBuilder.AddColumn<long>(
                name: "DialyActivityId",
                table: "DialyUserEvaluations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DialyUserEvaluations_DialyActivityId",
                table: "DialyUserEvaluations",
                column: "DialyActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_DialyActivities_DialyActivityId",
                table: "DialyUserEvaluations",
                column: "DialyActivityId",
                principalTable: "DialyActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_DialyActivities_DialyActivityId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_DialyUserEvaluations_DialyActivityId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropColumn(
                name: "DialyActivityId",
                table: "DialyUserEvaluations");

            migrationBuilder.AddColumn<long>(
                name: "TrainingId",
                table: "DialyUserEvaluations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DialyUserEvaluations_TrainingId",
                table: "DialyUserEvaluations",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_Trainings_TrainingId",
                table: "DialyUserEvaluations",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }
    }
}
