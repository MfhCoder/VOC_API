using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateSurveyDeliveryV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SurveyDeliveries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SurveyDeliveries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "SurveyDeliveries",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyDeliveries_UpdatedBy",
                table: "SurveyDeliveries",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyDeliveries_User_UpdatedBy",
                table: "SurveyDeliveries",
                column: "UpdatedBy",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyDeliveries_User_UpdatedBy",
                table: "SurveyDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_SurveyDeliveries_UpdatedBy",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SurveyDeliveries");
        }
    }
}
