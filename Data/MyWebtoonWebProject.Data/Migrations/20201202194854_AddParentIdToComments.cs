using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebtoonWebProject.Data.Migrations
{
    public partial class AddParentIdToComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Comments");
        }
    }
}
