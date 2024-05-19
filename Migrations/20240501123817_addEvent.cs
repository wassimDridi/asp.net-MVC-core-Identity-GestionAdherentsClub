using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionAdherentsClub.Migrations
{
    /// <inheritdoc />
    public partial class addEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Adherents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ClubEvents",
                columns: table => new
                {
                    ClubEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubEvents", x => x.ClubEventId);
                    table.ForeignKey(
                        name: "FK_ClubEvents_Clubs_ClubID",
                        column: x => x.ClubID,
                        principalTable: "Clubs",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubEvents_ClubID",
                table: "ClubEvents",
                column: "ClubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubEvents");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Adherents");
        }
    }
}
