﻿// <auto-generated />
using FolderEncryption.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FolderEncryption.Migrations
{
    [DbContext(typeof(FileEncryptionContext))]
    [Migration("20201203191638_DirectoryRenamed")]
    partial class DirectoryRenamed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10");

            modelBuilder.Entity("FolderEncryption.Models.EncryptionKey", b =>
                {
                    b.Property<int>("KeyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicKey")
                        .HasColumnType("TEXT");

                    b.HasKey("KeyId");

                    b.ToTable("EncryptionKeys");
                });

            modelBuilder.Entity("FolderEncryption.Models.Folder", b =>
                {
                    b.Property<int>("DirectoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("KeyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.HasKey("DirectoryId");

                    b.HasIndex("KeyId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("FolderEncryption.Models.Folder", b =>
                {
                    b.HasOne("FolderEncryption.Models.EncryptionKey", "Key")
                        .WithMany("Folders")
                        .HasForeignKey("KeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
