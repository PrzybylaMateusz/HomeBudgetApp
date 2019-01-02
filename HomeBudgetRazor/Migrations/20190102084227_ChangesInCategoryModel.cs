using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeBudgetRazor.Migrations
{
    public partial class ChangesInCategoryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Category");
        }
    }
}
