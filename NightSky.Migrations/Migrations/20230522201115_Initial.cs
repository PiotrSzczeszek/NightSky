using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NightSky.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Constellation",
                columns: table => new
                {
                    ConstallationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constellation", x => x.ConstallationId);
                });

            migrationBuilder.CreateTable(
                name: "SkyData",
                columns: table => new
                {
                    SkyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FogLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    CloudsLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecipitationType = table.Column<int>(type: "INTEGER", nullable: false),
                    DataDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkyData", x => x.SkyId);
                });

            migrationBuilder.CreateTable(
                name: "Star",
                columns: table => new
                {
                    StarId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StarName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Star", x => x.StarId);
                });

            migrationBuilder.CreateTable(
                name: "ConstellationStar",
                columns: table => new
                {
                    ConstallationsConstallationId = table.Column<int>(type: "INTEGER", nullable: false),
                    StarsStarId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstellationStar", x => new { x.ConstallationsConstallationId, x.StarsStarId });
                    table.ForeignKey(
                        name: "FK_ConstellationStar_Constellation_ConstallationsConstallationId",
                        column: x => x.ConstallationsConstallationId,
                        principalTable: "Constellation",
                        principalColumn: "ConstallationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstellationStar_Star_StarsStarId",
                        column: x => x.StarsStarId,
                        principalTable: "Star",
                        principalColumn: "StarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstellationStar_StarsStarId",
                table: "ConstellationStar",
                column: "StarsStarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstellationStar");

            migrationBuilder.DropTable(
                name: "SkyData");

            migrationBuilder.DropTable(
                name: "Constellation");

            migrationBuilder.DropTable(
                name: "Star");
        }
    }
}
