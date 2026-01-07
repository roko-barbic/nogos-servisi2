using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NogosServisi.Migrations
{
    /// <inheritdoc />
    public partial class FixGameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayPlayersAndJerseyNumbersJson",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomePlayersAndJerseyNumbersJson",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId1",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId1",
                table: "Players",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Games_GameId1",
                table: "Players",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Games_GameId1",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameId1",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "AwayPlayersAndJerseyNumbersJson",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomePlayersAndJerseyNumbersJson",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
