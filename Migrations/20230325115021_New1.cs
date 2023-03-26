using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Albums.Migrations
{
    /// <inheritdoc />
    public partial class New1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RecordLabelId",
                table: "Artist",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artist_RecordLabelId",
                table: "Artist",
                column: "RecordLabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artist_RecordLabel_RecordLabelId",
                table: "Artist",
                column: "RecordLabelId",
                principalTable: "RecordLabel",
                principalColumn: "RecordLabelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artist_RecordLabel_RecordLabelId",
                table: "Artist");

            migrationBuilder.DropIndex(
                name: "IX_Artist_RecordLabelId",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "RecordLabelId",
                table: "Artist");
        }
    }
}
