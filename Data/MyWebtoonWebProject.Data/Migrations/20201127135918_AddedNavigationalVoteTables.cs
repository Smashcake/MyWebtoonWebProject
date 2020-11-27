namespace MyWebtoonWebProject.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedNavigationalVoteTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "CommentVotes",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    CommentId = table.Column<string>(nullable: false),
                    Vote = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentVotes", x => new { x.ApplicationUserId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_CommentVotes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentVotes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeLikes",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    EpisodeId = table.Column<string>(nullable: false),
                    HasLiked = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeLikes", x => new { x.ApplicationUserId, x.EpisodeId });
                    table.ForeignKey(
                        name: "FK_EpisodeLikes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EpisodeLikes_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewVotes",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    ReviewId = table.Column<string>(nullable: false),
                    Vote = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewVotes", x => new { x.ApplicationUserId, x.ReviewId });
                    table.ForeignKey(
                        name: "FK_ReviewVotes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewVotes_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_CommentId",
                table: "CommentVotes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeLikes_EpisodeId",
                table: "EpisodeLikes",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewVotes_ReviewId",
                table: "ReviewVotes",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentVotes");

            migrationBuilder.DropTable(
                name: "EpisodeLikes");

            migrationBuilder.DropTable(
                name: "ReviewVotes");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
