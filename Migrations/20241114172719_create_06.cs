using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_dette_web.Migrations
{
    /// <inheritdoc />
    public partial class create_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_UserId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Dettes_Clients_ClientId",
                table: "Dettes");

            migrationBuilder.DropForeignKey(
                name: "FK_Paiements_Dettes_DetteId",
                table: "Paiements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paiements",
                table: "Paiements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dettes",
                table: "Dettes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Paiements",
                newName: "paiements");

            migrationBuilder.RenameTable(
                name: "Dettes",
                newName: "dettes");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "clients");

            migrationBuilder.RenameIndex(
                name: "IX_Paiements_DetteId",
                table: "paiements",
                newName: "IX_paiements_DetteId");

            migrationBuilder.RenameIndex(
                name: "IX_Dettes_ClientId",
                table: "dettes",
                newName: "IX_dettes_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_UserId",
                table: "clients",
                newName: "IX_clients_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paiements",
                table: "paiements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dettes",
                table: "dettes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients",
                table: "clients",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Libelle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prix = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "details",
                columns: table => new
                {
                    DetteId = table.Column<int>(type: "int", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    Qte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_details", x => new { x.DetteId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_details_articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_details_dettes_DetteId",
                        column: x => x.DetteId,
                        principalTable: "dettes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_details_ArticleId",
                table: "details",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_clients_users_UserId",
                table: "clients",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dettes_clients_ClientId",
                table: "dettes",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_paiements_dettes_DetteId",
                table: "paiements",
                column: "DetteId",
                principalTable: "dettes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_users_UserId",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "FK_dettes_clients_ClientId",
                table: "dettes");

            migrationBuilder.DropForeignKey(
                name: "FK_paiements_dettes_DetteId",
                table: "paiements");

            migrationBuilder.DropTable(
                name: "details");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paiements",
                table: "paiements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dettes",
                table: "dettes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clients",
                table: "clients");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "paiements",
                newName: "Paiements");

            migrationBuilder.RenameTable(
                name: "dettes",
                newName: "Dettes");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_paiements_DetteId",
                table: "Paiements",
                newName: "IX_Paiements_DetteId");

            migrationBuilder.RenameIndex(
                name: "IX_dettes_ClientId",
                table: "Dettes",
                newName: "IX_Dettes_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_clients_UserId",
                table: "Clients",
                newName: "IX_Clients_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paiements",
                table: "Paiements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dettes",
                table: "Dettes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_UserId",
                table: "Clients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dettes_Clients_ClientId",
                table: "Dettes",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiements_Dettes_DetteId",
                table: "Paiements",
                column: "DetteId",
                principalTable: "Dettes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
