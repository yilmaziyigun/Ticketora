using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketora.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Events");
        }
    }
}
