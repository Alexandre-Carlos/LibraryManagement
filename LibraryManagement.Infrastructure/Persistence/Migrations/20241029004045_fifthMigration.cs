using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 28, 21, 40, 44, 860, DateTimeKind.Local).AddTicks(805));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 28, 21, 40, 44, 860, DateTimeKind.Local).AddTicks(820));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 28, 21, 40, 44, 860, DateTimeKind.Local).AddTicks(821));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "Name", "Password", "Role", "Salt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 28, 21, 40, 44, 861, DateTimeKind.Local).AddTicks(5617), "cliente1@teste.com.br", false, "Cliente_1", "$HASH|V1$10000$hkJQltolSmXA86IJW76J46fST28x1inv1NDGHrmlMKlBVN2c", "Client", "hkJQltolSmXA86IJW76J4w==" },
                    { 2, new DateTime(2024, 10, 28, 21, 40, 44, 861, DateTimeKind.Local).AddTicks(5625), "cliente2@teste.com.br", false, "cliente_2", "$HASH|V1$10000$GDDUHWb1no2cn7BGXZGQ22J4IqpTX5Ng8bH+fV12BhnFl0CV", "Client", "GDDUHWb1no2cn7BGXZGQ2w==" },
                    { 99, new DateTime(2024, 10, 28, 21, 40, 44, 861, DateTimeKind.Local).AddTicks(5626), "admin@teste.com.br", false, "Admin", "$HASH|V1$10000$GDDUHWb1no2cn7BGXZGQ22J4IqpTX5Ng8bH+fV12BhnFl0CV", "Manager", "GDDUHWb1no2cn7BGXZGQ2w==" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 20, 1, 40, 522, DateTimeKind.Local).AddTicks(2626));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 20, 1, 40, 522, DateTimeKind.Local).AddTicks(2642));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 20, 1, 40, 522, DateTimeKind.Local).AddTicks(2643));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "Name", "Password", "Salt" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 10, 23, 20, 1, 40, 523, DateTimeKind.Local).AddTicks(4417), "cliente1@teste.com.br", false, "Cliente_1", "$HASH|V1$10000$hkJQltolSmXA86IJW76J46fST28x1inv1NDGHrmlMKlBVN2c", "hkJQltolSmXA86IJW76J4w==" },
                    { 5, new DateTime(2024, 10, 23, 20, 1, 40, 523, DateTimeKind.Local).AddTicks(4425), "cliente2@teste.com.br", false, "cliente_2", "$HASH|V1$10000$GDDUHWb1no2cn7BGXZGQ22J4IqpTX5Ng8bH+fV12BhnFl0CV", "GDDUHWb1no2cn7BGXZGQ2w==" }
                });
        }
    }
}
