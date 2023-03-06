using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSistemaSalao.Migrations
{
    /// <inheritdoc />
    public partial class SecondLoginCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_login",
                table: "login");

            migrationBuilder.RenameTable(
                name: "login",
                newName: "Login");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Login",
                table: "Login",
                column: "LoginID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Login",
                table: "Login");

            migrationBuilder.RenameTable(
                name: "Login",
                newName: "login");

            migrationBuilder.AddPrimaryKey(
                name: "PK_login",
                table: "login",
                column: "LoginID");
        }
    }
}
