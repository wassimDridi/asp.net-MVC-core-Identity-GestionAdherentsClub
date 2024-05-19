using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionAdherentsClub.Migrations
{
    /// <inheritdoc />
    public partial class addusertoadherent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "Adherents",
                newName: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Adherents",
                newName: "email");
        }
    }
}
