﻿// <auto-generated />
using System;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagement.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(LibraryManagementDbContext))]
    [Migration("20241029004045_fifthMigration")]
    partial class fifthMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagement.Core.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("YearPublished")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Books", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Robert C. Martin",
                            CreatedAt = new DateTime(2024, 10, 28, 21, 40, 44, 860, DateTimeKind.Local).AddTicks(805),
                            IsDeleted = false,
                            Isbn = "978-8576082675",
                            Quantity = 5,
                            Title = "Código Limpo: Habilidades Práticas do Agile Software",
                            YearPublished = 2009
                        },
                        new
                        {
                            Id = 2,
                            Author = "Robert C. Martin",
                            CreatedAt = new DateTime(2024, 10, 28, 21, 40, 44, 860, DateTimeKind.Local).AddTicks(820),
                            IsDeleted = false,
                            Isbn = "978-8550804606",
                            Quantity = 3,
                            Title = "Arquitetura Limpa: o Guia do Artesão Para Estrutura e Design de Software",
                            YearPublished = 2019
                        },
                        new
                        {
                            Id = 3,
                            Author = " Aditya Y. Bhargava",
                            CreatedAt = new DateTime(2024, 10, 28, 21, 40, 44, 860, DateTimeKind.Local).AddTicks(821),
                            IsDeleted = false,
                            Isbn = "978-8575225639",
                            Quantity = 8,
                            Title = "Entendendo Algoritmos: Um Guia Ilustrado Para Programadores e Outros Curiosos",
                            YearPublished = 2019
                        });
                });

            modelBuilder.Entity("LibraryManagement.Core.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<DateTime>("DateOfLoan")
                        .HasColumnType("date");

                    b.Property<int>("DaysOfDelay")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDateLoan")
                        .HasColumnType("date");

                    b.Property<int>("IdBook")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("IdBook");

                    b.HasIndex("IdUser");

                    b.ToTable("Loans", (string)null);
                });

            modelBuilder.Entity("LibraryManagement.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 10, 28, 21, 40, 44, 861, DateTimeKind.Local).AddTicks(5617),
                            Email = "cliente1@teste.com.br",
                            IsDeleted = false,
                            Name = "Cliente_1",
                            Password = "$HASH|V1$10000$hkJQltolSmXA86IJW76J46fST28x1inv1NDGHrmlMKlBVN2c",
                            Role = "Client",
                            Salt = "hkJQltolSmXA86IJW76J4w=="
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 10, 28, 21, 40, 44, 861, DateTimeKind.Local).AddTicks(5625),
                            Email = "cliente2@teste.com.br",
                            IsDeleted = false,
                            Name = "cliente_2",
                            Password = "$HASH|V1$10000$GDDUHWb1no2cn7BGXZGQ22J4IqpTX5Ng8bH+fV12BhnFl0CV",
                            Role = "Client",
                            Salt = "GDDUHWb1no2cn7BGXZGQ2w=="
                        },
                        new
                        {
                            Id = 99,
                            CreatedAt = new DateTime(2024, 10, 28, 21, 40, 44, 861, DateTimeKind.Local).AddTicks(5626),
                            Email = "admin@teste.com.br",
                            IsDeleted = false,
                            Name = "Admin",
                            Password = "$HASH|V1$10000$GDDUHWb1no2cn7BGXZGQ22J4IqpTX5Ng8bH+fV12BhnFl0CV",
                            Role = "Manager",
                            Salt = "GDDUHWb1no2cn7BGXZGQ2w=="
                        });
                });

            modelBuilder.Entity("LibraryManagement.Core.Entities.Loan", b =>
                {
                    b.HasOne("LibraryManagement.Core.Entities.Book", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LibraryManagement.Core.Entities.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryManagement.Core.Entities.Book", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("LibraryManagement.Core.Entities.User", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
