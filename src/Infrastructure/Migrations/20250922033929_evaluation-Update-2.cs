using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class evaluationUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_DialyActivities_DialyActivityId",
                table: "DialyUserEvaluations");

            migrationBuilder.AlterColumn<long>(
                name: "DialyActivityId",
                table: "DialyUserEvaluations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_DialyActivities_DialyActivityId",
                table: "DialyUserEvaluations",
                column: "DialyActivityId",
                principalTable: "DialyActivities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_DialyActivities_DialyActivityId",
                table: "DialyUserEvaluations");

            migrationBuilder.AlterColumn<long>(
                name: "DialyActivityId",
                table: "DialyUserEvaluations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
