using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FolderEncryption.Migrations
{
    public partial class ChangedEncryptedKeyToBytes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "EncryptedKey",
                table: "EncryptionKeys",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EncryptedKey",
                table: "EncryptionKeys",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
