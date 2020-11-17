namespace MyWebtoonWebProject.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class IncludedCompletionDateToWebtoon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Webtoons",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Webtoons");
        }
    }
}
