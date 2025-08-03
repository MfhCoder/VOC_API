using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MessageContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageContent",
                table: "Surveys",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Surveys",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisibleMerchantInfo",
                table: "Surveys",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantId",
                table: "Merchants",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageContent",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "VisibleMerchantInfo",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "Merchants");
        }
    }
}
