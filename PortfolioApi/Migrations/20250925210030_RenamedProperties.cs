using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senderName",
                table: "Messages",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "senderEmail",
                table: "Messages",
                newName: "SenderEmail");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Messages",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "Messages",
                newName: "senderName");

            migrationBuilder.RenameColumn(
                name: "SenderEmail",
                table: "Messages",
                newName: "senderEmail");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Messages",
                newName: "content");
        }
    }
}
