using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeNameFromSurveyBatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyDeliveries_DeliveryLinks_LinkId",
                table: "SurveyDeliveries");

            migrationBuilder.DropTable(
                name: "DeliveryLinks");

            migrationBuilder.DropIndex(
                name: "IX_SurveyDeliveries_LinkId",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "LinkId",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SurveyBatches");

            migrationBuilder.AlterColumn<string>(
                name: "EncryptionToken",
                table: "SurveyDeliveries",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LinkExpirationDate",
                table: "SurveyDeliveries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SurveyURL",
                table: "SurveyDeliveries",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Sentiment",
                table: "Feedbacks",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkExpirationDate",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "SurveyURL",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "Sentiment",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "EncryptionToken",
                table: "SurveyDeliveries",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "LinkId",
                table: "SurveyDeliveries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SurveyBatches",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DeliveryLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LongUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryLinks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyDeliveries_LinkId",
                table: "SurveyDeliveries",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyDeliveries_DeliveryLinks_LinkId",
                table: "SurveyDeliveries",
                column: "LinkId",
                principalTable: "DeliveryLinks",
                principalColumn: "Id");
        }
    }
}
