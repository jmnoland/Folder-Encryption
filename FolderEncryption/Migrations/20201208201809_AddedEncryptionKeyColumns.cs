using Microsoft.EntityFrameworkCore.Migrations;

namespace FolderEncryption.Migrations
{
    public partial class AddedEncryptionKeyColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "EncryptionKeys",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicKeyName",
                table: "EncryptionKeys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "EncryptionKeys");

            migrationBuilder.DropColumn(
                name: "PublicKeyName",
                table: "EncryptionKeys");
        }
    }
}
