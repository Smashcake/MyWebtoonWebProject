using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebtoonWebProject.Data.Migrations
{
    public partial class AddNumberColumnToReviewAndComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewNumber",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentNumber",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewNumber",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CommentNumber",
                table: "Comments");
        }
    }
}
