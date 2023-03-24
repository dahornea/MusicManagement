using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Albums.Migrations
{
    /// <inheritdoc />
    public partial class OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Album",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ArtistId1",
                table: "Album",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistId1",
                table: "Album",
                column: "ArtistId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artist_ArtistId1",
                table: "Album",
                column: "ArtistId1",
                principalTable: "Artist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artist_ArtistId1",
                table: "Album");

            migrationBuilder.DropIndex(
                name: "IX_Album_ArtistId1",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "ArtistId1",
                table: "Album");
        }
    }
}
