using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NightSky.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationStar_Star_StarsStarId",
                table: "ConstellationStar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Star",
                table: "Star");

            migrationBuilder.RenameTable(
                name: "Star",
                newName: "Stars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stars",
                table: "Stars",
                column: "StarId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationStar_Stars_StarsStarId",
                table: "ConstellationStar",
                column: "StarsStarId",
                principalTable: "Stars",
                principalColumn: "StarId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationStar_Stars_StarsStarId",
                table: "ConstellationStar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stars",
                table: "Stars");

            migrationBuilder.RenameTable(
                name: "Stars",
                newName: "Star");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Star",
                table: "Star",
                column: "StarId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationStar_Star_StarsStarId",
                table: "ConstellationStar",
                column: "StarsStarId",
                principalTable: "Star",
                principalColumn: "StarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
