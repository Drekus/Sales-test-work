using Microsoft.EntityFrameworkCore.Migrations;

namespace Sales.Migrations
{
    public partial class RemoveOrderedBookAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookAmount",
                table: "OrderedBook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookAmount",
                table: "OrderedBook",
                nullable: false,
                defaultValue: 0);
        }
    }
}
