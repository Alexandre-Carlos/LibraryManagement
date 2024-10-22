using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YearPublished = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdBook = table.Column<int>(type: "int", nullable: false),
                    DateOfLoan = table.Column<DateTime>(type: "date", nullable: false),
                    EndDateLoan = table.Column<DateTime>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "date", nullable: false),
                    DaysOfDelay = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Books_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CreatedAt", "IsDeleted", "Isbn", "Quantity", "Title", "YearPublished" },
                values: new object[,]
                {
                    { 1, "Robert C. Martin", new DateTime(2024, 10, 14, 20, 49, 1, 785, DateTimeKind.Local).AddTicks(2509), false, "978-8576082675", 5, "Código Limpo: Habilidades Práticas do Agile Software", 2009 },
                    { 2, "Robert C. Martin", new DateTime(2024, 10, 14, 20, 49, 1, 785, DateTimeKind.Local).AddTicks(2525), false, "978-8550804606", 3, "Arquitetura Limpa: o Guia do Artesão Para Estrutura e Design de Software", 2019 },
                    { 3, " Aditya Y. Bhargava", new DateTime(2024, 10, 14, 20, 49, 1, 785, DateTimeKind.Local).AddTicks(2526), false, "978-8575225639", 8, "Entendendo Algoritmos: Um Guia Ilustrado Para Programadores e Outros Curiosos", 2019 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 14, 20, 49, 1, 787, DateTimeKind.Local).AddTicks(86), "cliente1@teste.com.br", false, "Cliente1" },
                    { 2, new DateTime(2024, 10, 14, 20, 49, 1, 787, DateTimeKind.Local).AddTicks(109), "cliente2@teste.com.br", false, "cliente2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_IdBook",
                table: "Loans",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_IdUser",
                table: "Loans",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
