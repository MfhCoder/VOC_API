using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveyFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnName",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "ColumnValue",
                table: "SurveyFilters");

            migrationBuilder.AddColumn<bool>(
                name: "Connected_DB",
                table: "Surveys",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Surveys",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "SurveyFilters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ledgers",
                table: "SurveyFilters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "License",
                table: "SurveyFilters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "SurveyFilters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxTenureInDays",
                table: "SurveyFilters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinTenureInDays",
                table: "SurveyFilters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Products",
                table: "SurveyFilters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Search",
                table: "SurveyFilters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "SurveyFilters",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Connected_DB",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "Ledgers",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "License",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "MaxTenureInDays",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "MinTenureInDays",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "Products",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "Search",
                table: "SurveyFilters");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "SurveyFilters");

            migrationBuilder.AddColumn<string>(
                name: "ColumnName",
                table: "SurveyFilters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColumnValue",
                table: "SurveyFilters",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
