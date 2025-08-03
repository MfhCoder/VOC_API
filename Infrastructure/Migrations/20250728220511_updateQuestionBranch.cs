using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateQuestionBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionBranches");

            migrationBuilder.AddColumn<int>(
                name: "TriggerOptionId",
                table: "SurveyQuestions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_TriggerOptionId",
                table: "SurveyQuestions",
                column: "TriggerOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_QuestionOptions_TriggerOptionId",
                table: "SurveyQuestions",
                column: "TriggerOptionId",
                principalTable: "QuestionOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_QuestionOptions_TriggerOptionId",
                table: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestions_TriggerOptionId",
                table: "SurveyQuestions");

            migrationBuilder.DropColumn(
                name: "TriggerOptionId",
                table: "SurveyQuestions");

            migrationBuilder.CreateTable(
                name: "QuestionBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentQuestionId = table.Column<int>(type: "integer", nullable: false),
                    TriggerOptionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionBranches_QuestionOptions_TriggerOptionId",
                        column: x => x.TriggerOptionId,
                        principalTable: "QuestionOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionBranches_SurveyQuestions_ParentQuestionId",
                        column: x => x.ParentQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBranches_ParentQuestionId",
                table: "QuestionBranches",
                column: "ParentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBranches_TriggerOptionId",
                table: "QuestionBranches",
                column: "TriggerOptionId");
        }
    }
}
