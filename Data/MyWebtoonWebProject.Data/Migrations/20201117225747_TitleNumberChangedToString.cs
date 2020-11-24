namespace MyWebtoonWebProject.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class TitleNumberChangedToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TitleNumber",
                table: "Webtoons",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TitleNumber",
                table: "Webtoons",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
