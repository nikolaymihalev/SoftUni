using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P02_FootballBetting.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_PlayerStatistics_Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Towns_TownId1",
                table: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Towns_TownId1",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "TownId1",
                table: "Towns");

            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ScoredGoals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => new { x.GameId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStatistics");

            migrationBuilder.AddColumn<int>(
                name: "TownId1",
                table: "Towns",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Towns_TownId1",
                table: "Towns",
                column: "TownId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Towns_TownId1",
                table: "Towns",
                column: "TownId1",
                principalTable: "Towns",
                principalColumn: "TownId");
        }
    }
}
