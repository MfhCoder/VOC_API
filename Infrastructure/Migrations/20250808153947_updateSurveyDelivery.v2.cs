using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateSurveyDeliveryv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "SurveyDeliveries",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyDeliveries_SurveyId",
                table: "SurveyDeliveries",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyDeliveries_Surveys_SurveyId",
                table: "SurveyDeliveries",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyDeliveries_Surveys_SurveyId",
                table: "SurveyDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_SurveyDeliveries_SurveyId",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "SurveyDeliveries");
        }
    }
}
