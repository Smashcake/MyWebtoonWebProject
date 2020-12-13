namespace MyWebtoonWebProject.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedWebtoonRatingsAndEpisodeViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Webtoons");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Episodes");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Pages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileExtention",
                table: "Pages",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Pages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EpisodesViews",
                columns: table => new
                {
                    EpisodeId = table.Column<string>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    LastViewedOn = table.Column<DateTime>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodesViews", x => new { x.ApplicationUserId, x.EpisodeId });
                    table.ForeignKey(
                        name: "FK_EpisodesViews_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EpisodesViews_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebtoonsRatings",
                columns: table => new
                {
                    WebtoonId = table.Column<string>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    RatingValue = table.Column<byte>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebtoonsRatings", x => new { x.ApplicationUserId, x.WebtoonId });
                    table.ForeignKey(
                        name: "FK_WebtoonsRatings_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebtoonsRatings_Webtoons_WebtoonId",
                        column: x => x.WebtoonId,
                        principalTable: "Webtoons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EpisodesViews_EpisodeId",
                table: "EpisodesViews",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_WebtoonsRatings_WebtoonId",
                table: "WebtoonsRatings",
                column: "WebtoonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EpisodesViews");

            migrationBuilder.DropTable(
                name: "WebtoonsRatings");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "FileExtention",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Pages");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Webtoons",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
