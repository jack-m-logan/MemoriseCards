using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoriseCards.Migrations
{
    /// <inheritdoc />
    public partial class ModifyIndexesToAvoidKeyViolations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Card_Suit_Rank",
                table: "Card");

            migrationBuilder.CreateIndex(
                name: "IX_Card_Id_Suit_Rank",
                table: "Card",
                columns: new[] { "Id", "Suit", "Rank" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Card_Id_Suit_Rank",
                table: "Card");

            migrationBuilder.CreateIndex(
                name: "IX_Card_Suit_Rank",
                table: "Card",
                columns: new[] { "Suit", "Rank" },
                unique: true);
        }
    }
}
