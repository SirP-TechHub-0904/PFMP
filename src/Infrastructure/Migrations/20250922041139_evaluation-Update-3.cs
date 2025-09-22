using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class evaluationUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DialyEvaluationQuestionOption",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    DialyEvaluationQuestionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialyEvaluationQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialyEvaluationQuestionOption_DialyEvaluationQuestions_DialyEvaluationQuestionId",
                        column: x => x.DialyEvaluationQuestionId,
                        principalTable: "DialyEvaluationQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DialyEvaluationQuestionOption_DialyEvaluationQuestionId",
                table: "DialyEvaluationQuestionOption",
                column: "DialyEvaluationQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialyEvaluationQuestionOption");
        }
    }
}
