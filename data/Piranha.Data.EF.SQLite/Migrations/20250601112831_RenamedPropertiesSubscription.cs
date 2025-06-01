using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Piranha.Data.EF.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class RenamedPropertiesSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Piranha_Subscriptions_EventType_Filter",
                table: "Piranha_Subscriptions");

            migrationBuilder.DropColumn(
                name: "Filter",
                table: "Piranha_Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "EventStatus",
                table: "Piranha_Subscriptions",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Piranha_Subscriptions",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Subscriptions_EventType_EventStatus_Tags",
                table: "Piranha_Subscriptions",
                columns: new[] { "EventType", "EventStatus", "Tags" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Piranha_Subscriptions_EventType_EventStatus_Tags",
                table: "Piranha_Subscriptions");

            migrationBuilder.DropColumn(
                name: "EventStatus",
                table: "Piranha_Subscriptions");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Piranha_Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "Filter",
                table: "Piranha_Subscriptions",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Subscriptions_EventType_Filter",
                table: "Piranha_Subscriptions",
                columns: new[] { "EventType", "Filter" },
                unique: true);
        }
    }
}
