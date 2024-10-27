using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class thirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 19, 14, 1, 971, DateTimeKind.Local).AddTicks(8426));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 19, 14, 1, 971, DateTimeKind.Local).AddTicks(8441));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 19, 14, 1, 971, DateTimeKind.Local).AddTicks(8442));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 10, 23, 19, 14, 1, 972, DateTimeKind.Local).AddTicks(7990), "$HASH|V1$10000$hkJQltolSmXA86IJW76J46fST28x1inv1NDGHrmlMKlBVN2c", "hkJQltolSmXA86IJW76J4w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 10, 23, 19, 14, 1, 972, DateTimeKind.Local).AddTicks(7998), "$HASH|V1$10000$GDDUHWb1no2cn7BGXZGQ22J4IqpTX5Ng8bH+fV12BhnFl0CV", "GDDUHWb1no2cn7BGXZGQ2w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 20, 12, 4, 24, 716, DateTimeKind.Local).AddTicks(275));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 20, 12, 4, 24, 716, DateTimeKind.Local).AddTicks(293));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 20, 12, 4, 24, 716, DateTimeKind.Local).AddTicks(294));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 20, 12, 4, 24, 717, DateTimeKind.Local).AddTicks(4395));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 20, 12, 4, 24, 717, DateTimeKind.Local).AddTicks(4403));
        }
    }
}
