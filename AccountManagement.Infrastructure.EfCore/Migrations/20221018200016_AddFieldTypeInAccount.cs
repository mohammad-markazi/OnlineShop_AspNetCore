using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EfCore.Migrations
{
    public partial class AddFieldTypeInAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Type",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Accounts");
        }
    }
}
