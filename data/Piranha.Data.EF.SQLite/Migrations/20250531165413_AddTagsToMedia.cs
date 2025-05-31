using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Piranha.Data.EF.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class AddTagsToMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Piranha_Media",
                type: "TEXT",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Piranha_Media",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 512);
        }
    }
}
