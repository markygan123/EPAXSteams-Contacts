using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EPAXSteamsContacts.Migrations
{
    public partial class AddLifecyclesDropdownEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LifecycleStagesEnum",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LifecycleStagesEnum",
                table: "Contacts");
        }
    }
}
