using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoriseCards.Migrations
{
    /// <inheritdoc />
    public partial class MakeDeckUserIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
            name: "UserId",
            table: "Deck",
            type: "integer",
            nullable: true, // Make UserId nullable
            oldClrType: typeof(int),
            oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
