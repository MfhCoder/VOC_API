using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateSurveyDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyDeliveries_DeliveryLinks_LinkId",
                table: "SurveyDeliveries");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SurveyBatches");

            migrationBuilder.AlterColumn<int>(
                name: "RetryCount",
                table: "SurveyDeliveries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "LinkId",
                table: "SurveyDeliveries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "EncryptionToken",
                table: "SurveyDeliveries",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyDeliveries_DeliveryLinks_LinkId",
                table: "SurveyDeliveries",
                column: "LinkId",
                principalTable: "DeliveryLinks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyDeliveries_DeliveryLinks_LinkId",
                table: "SurveyDeliveries");

            migrationBuilder.AlterColumn<int>(
                name: "RetryCount",
                table: "SurveyDeliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LinkId",
                table: "SurveyDeliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SurveyBatches",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyDeliveries_DeliveryLinks_LinkId",
                table: "SurveyDeliveries",
                column: "LinkId",
                principalTable: "DeliveryLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
