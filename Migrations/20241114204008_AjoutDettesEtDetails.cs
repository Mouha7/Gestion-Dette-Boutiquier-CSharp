using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_dette_web.Migrations
{
    /// <inheritdoc />
    public partial class AjoutDettesEtDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Montant",
                table: "details",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Montant",
                table: "details");
        }
    }
}
