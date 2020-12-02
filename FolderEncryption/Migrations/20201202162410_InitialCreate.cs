using Microsoft.EntityFrameworkCore.Migrations;

namespace FolderEncryption.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncryptionKeys",
                columns: table => new
                {
                    KeyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicKey = table.Column<string>(nullable: true),
                    CreateDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptionKeys", x => x.KeyId);
                });

            migrationBuilder.CreateTable(
                name: "Directories",
                columns: table => new
                {
                    DirectoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KeyId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directories", x => x.DirectoryId);
                    table.ForeignKey(
                        name: "FK_Directories_EncryptionKeys_KeyId",
                        column: x => x.KeyId,
                        principalTable: "EncryptionKeys",
                        principalColumn: "KeyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Directories_KeyId",
                table: "Directories",
                column: "KeyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Directories");

            migrationBuilder.DropTable(
                name: "EncryptionKeys");
        }
    }
}
