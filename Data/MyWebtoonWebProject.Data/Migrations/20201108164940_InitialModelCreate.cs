using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebtoonWebProject.Data.Migrations
{
    public partial class InitialModelCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Webtoons",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Synopsis = table.Column<string>(maxLength: 600, nullable: false),
                    CoverPhoto = table.Column<string>(nullable: false),
                    Rating = table.Column<decimal>(nullable: false),
                    GenreId = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true),
                    UploadDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Webtoons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Webtoons_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Webtoons_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Views = table.Column<int>(nullable: false),
                    UploadedOn = table.Column<DateTime>(nullable: false),
                    WebtoonId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Webtoons_WebtoonId",
                        column: x => x.WebtoonId,
                        principalTable: "Webtoons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ReviewAuthorId = table.Column<string>(nullable: true),
                    WebtoonId = table.Column<string>(nullable: true),
                    ReviewInfo = table.Column<string>(maxLength: 800, nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_ReviewAuthorId",
                        column: x => x.ReviewAuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Webtoons_WebtoonId",
                        column: x => x.WebtoonId,
                        principalTable: "Webtoons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebtoonsSubscribers",
                columns: table => new
                {
                    SubscriberId = table.Column<string>(nullable: false),
                    WebtoonId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebtoonsSubscribers", x => new { x.SubscriberId, x.WebtoonId });
                    table.ForeignKey(
                        name: "FK_WebtoonsSubscribers_AspNetUsers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebtoonsSubscribers_Webtoons_WebtoonId",
                        column: x => x.WebtoonId,
                        principalTable: "Webtoons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CommentAuthorId = table.Column<string>(nullable: true),
                    EpisodeId = table.Column<string>(nullable: true),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false),
                    PostedOn = table.Column<DateTime>(nullable: false),
                    CommentInfo = table.Column<string>(maxLength: 300, nullable: false),
                    CommentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_CommentAuthorId",
                        column: x => x.CommentAuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EpisodeId = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentAuthorId",
                table: "Comments",
                column: "CommentAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EpisodeId",
                table: "Comments",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IsDeleted",
                table: "Comments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_IsDeleted",
                table: "Episodes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_WebtoonId",
                table: "Episodes",
                column: "WebtoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_EpisodeId",
                table: "Pages",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IsDeleted",
                table: "Reviews",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewAuthorId",
                table: "Reviews",
                column: "ReviewAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WebtoonId",
                table: "Reviews",
                column: "WebtoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Webtoons_AuthorId",
                table: "Webtoons",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Webtoons_GenreId",
                table: "Webtoons",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Webtoons_IsDeleted",
                table: "Webtoons",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WebtoonsSubscribers_WebtoonId",
                table: "WebtoonsSubscribers",
                column: "WebtoonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "WebtoonsSubscribers");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Webtoons");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_IsDeleted",
                table: "Settings",
                column: "IsDeleted");
        }
    }
}
