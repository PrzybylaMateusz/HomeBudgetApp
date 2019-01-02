using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeBudgetRazor.Migrations
{
    public partial class neOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Expense",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Expense");
        }
    }
}
