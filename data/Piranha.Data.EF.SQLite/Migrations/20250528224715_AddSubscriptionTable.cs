using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Piranha.Data.EF.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Piranha_Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 64, nullable: false),
                    EventType = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Filter = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    CallbackUrl = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Subscriptions_EventType_Filter",
                table: "Piranha_Subscriptions",
                columns: new[] { "EventType", "Filter" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Piranha_Subscriptions");
        }
    }
}
