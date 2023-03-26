﻿// <auto-generated />
using System;
using AniMedia.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AniMedia.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230326164208_Binary files")]
    partial class Binaryfiles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AniMedia.Domain.Entities.BinaryFileEntity", b =>
                {
                    b.Property<Guid>("UID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UID");

                    b.ToTable("BinaryFiles");
                });

            modelBuilder.Entity("AniMedia.Domain.Entities.SessionEntity", b =>
                {
                    b.Property<Guid>("UID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RefreshToken")
                        .HasColumnType("uuid");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserUid")
                        .HasColumnType("uuid");

                    b.HasKey("UID");

                    b.HasIndex("UserUid");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("AniMedia.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("UID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarLink")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.HasKey("UID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UID = new Guid("9fd09841-ef23-4b37-9237-8eed04ff8d8d"),
                            AvatarLink = "",
                            FirstName = "",
                            Nickname = "de1ete",
                            PasswordHash = "iDR0tLFtovM0IcDvxocHaCRCZZ6RDO3HVuUx3pIO0vDw3WP5qlLBSjVI3PmaflP5G1dVZEfE3oS4KB8IaUVQwg==",
                            PasswordSalt = "wwkDoDuq1buKb/Cca65BVfLEeNkp5axgOpXkd25kDs6uCEkhtpG16z9UXxtvNBC5UnbdfHPPyduPKHjdNNsbFvkBtVR176zu4YHJWqAl9nN9By1VsUZpf+jIR5/H40teb2y+oiATCbM+zhhaBbRK8N+JVf/KxWyfPtbpJCw84X0=",
                            SecondName = ""
                        },
                        new
                        {
                            UID = new Guid("757cafc6-b801-490f-87f8-f07e75fdb834"),
                            AvatarLink = "",
                            FirstName = "",
                            Nickname = "common",
                            PasswordHash = "iDR0tLFtovM0IcDvxocHaCRCZZ6RDO3HVuUx3pIO0vDw3WP5qlLBSjVI3PmaflP5G1dVZEfE3oS4KB8IaUVQwg==",
                            PasswordSalt = "wwkDoDuq1buKb/Cca65BVfLEeNkp5axgOpXkd25kDs6uCEkhtpG16z9UXxtvNBC5UnbdfHPPyduPKHjdNNsbFvkBtVR176zu4YHJWqAl9nN9By1VsUZpf+jIR5/H40teb2y+oiATCbM+zhhaBbRK8N+JVf/KxWyfPtbpJCw84X0=",
                            SecondName = ""
                        });
                });

            modelBuilder.Entity("AniMedia.Domain.Entities.SessionEntity", b =>
                {
                    b.HasOne("AniMedia.Domain.Entities.UserEntity", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AniMedia.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
