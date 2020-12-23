using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FolderEncryption.Migrations
{
    public partial class AddedSysmeticKeyFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncryptedKey",
                table: "EncryptionKeys",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "IV",
                table: "EncryptionKeys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedKey",
                table: "EncryptionKeys");

            migrationBuilder.DropColumn(
                name: "IV",
                table: "EncryptionKeys");
        }
    }
}
