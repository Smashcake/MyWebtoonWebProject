using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebtoonWebProject.Data.Migrations
{
    public partial class AddedPageNumberToPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EpisodeId",
                table: "Pages",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PageNumber",
                table: "Pages",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageNumber",
                table: "Pages");

            migrationBuilder.AlterColumn<string>(
                name: "EpisodeId",
                table: "Pages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
