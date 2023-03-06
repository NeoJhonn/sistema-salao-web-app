using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSistemaSalao.Migrations
{
    /// <inheritdoc />
    public partial class LoginInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "professional",
                table: "Client",
                newName: "Professional");

            migrationBuilder.RenameColumn(
                name: "FoneNumber",
                table: "Client",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "Schedule",
                type: "varchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "Professional",
                table: "Schedule",
                type: "varchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "EndTime",
                table: "Schedule",
                type: "varchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    LoginID = table.Column<string>(type: "varchar(450)", nullable: false),
                    Password = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.LoginID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.RenameColumn(
                name: "Professional",
                table: "Client",
                newName: "professional");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Client",
                newName: "FoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "Schedule",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Professional",
                table: "Schedule",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndTime",
                table: "Schedule",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true);
        }
    }
}
