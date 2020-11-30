using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebtoonWebProject.Data.Migrations
{
    public partial class AddEpisodeNumberColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EpisodeNumber",
                table: "Episodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeNumber",
                table: "Episodes");
        }
    }
}
