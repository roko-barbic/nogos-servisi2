using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NogosServisi.Migrations
{
    /// <inheritdoc />
    public partial class FixGameModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeOfMatch",
                table: "Games",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeOfMatch",
                table: "Games");
        }
    }
}
