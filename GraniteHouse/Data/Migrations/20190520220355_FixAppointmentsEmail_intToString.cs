using Microsoft.EntityFrameworkCore.Migrations;

namespace GraniteHouse.Data.Migrations
{
    public partial class FixAppointmentsEmail_intToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerEmail",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerEmail",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
