using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoriseCards.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deck_User_UserId",
                table: "Deck");

            migrationBuilder.DropForeignKey(
                name: "FK_POA_Card_CardId",
                table: "POA");

            migrationBuilder.DropForeignKey(
                name: "FK_Round_Deck_DeckId",
                table: "Round");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Round_RoundId",
                table: "Score");

            migrationBuilder.AddForeignKey(
                name: "FK_Deck_User_UserId",
                table: "Deck",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_POA_Card_CardId",
                table: "POA",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Deck_DeckId",
                table: "Round",
                column: "DeckId",
                principalTable: "Deck",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Round_RoundId",
                table: "Score",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deck_User_UserId",
                table: "Deck");

            migrationBuilder.DropForeignKey(
                name: "FK_POA_Card_CardId",
                table: "POA");

            migrationBuilder.DropForeignKey(
                name: "FK_Round_Deck_DeckId",
                table: "Round");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Round_RoundId",
                table: "Score");

            migrationBuilder.AddForeignKey(
                name: "FK_Deck_User_UserId",
                table: "Deck",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_POA_Card_CardId",
                table: "POA",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Deck_DeckId",
                table: "Round",
                column: "DeckId",
                principalTable: "Deck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Round_RoundId",
                table: "Score",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
