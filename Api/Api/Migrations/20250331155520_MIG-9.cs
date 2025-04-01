using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class MIG9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Game_id",
                table: "Images",
                nullable: false
            );

            migrationBuilder.AddColumn<string>(
                name: "PublisherImage",
                table: "Publishers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Game_id",
                table: "Images",
                column: "Game_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Games_Game_id",
                table: "Images",
                column: "Game_id",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Games_Game_id",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_Game_id",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PublisherImage",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Game_id",
                table: "Images"
            );

            migrationBuilder.AddColumn<string>(
                name: "Entity_type",
                table: "Images",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
