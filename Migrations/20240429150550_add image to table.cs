using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionAdherentsClub.Migrations
{
    /// <inheritdoc />
    public partial class addimagetotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "img",
                table: "Adherents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "img",
                table: "Adherents");
        }
    }
}
