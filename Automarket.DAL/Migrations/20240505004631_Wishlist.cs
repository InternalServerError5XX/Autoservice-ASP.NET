using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Automarket.DAL.Migrations
{
    public partial class Wishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 5, 5, 3, 46, 31, 315, DateTimeKind.Local).AddTicks(128), new DateTime(2024, 5, 5, 3, 46, 31, 315, DateTimeKind.Local).AddTicks(165) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 5, 5, 3, 46, 31, 315, DateTimeKind.Local).AddTicks(230), new DateTime(2024, 5, 5, 3, 46, 31, 315, DateTimeKind.Local).AddTicks(233) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 5, 5, 3, 46, 31, 315, DateTimeKind.Local).AddTicks(253), new DateTime(2024, 5, 5, 3, 46, 31, 315, DateTimeKind.Local).AddTicks(255) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 5, 5, 3, 46, 31, 315, DateTimeKind.Local).AddTicks(274), new DateTime(2024, 5, 5, 3, 46, 31, 315, DateTimeKind.Local).AddTicks(275) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 4, 15, 1, 42, 48, 304, DateTimeKind.Local).AddTicks(6978), new DateTime(2024, 4, 15, 1, 42, 48, 304, DateTimeKind.Local).AddTicks(7015) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 4, 15, 1, 42, 48, 304, DateTimeKind.Local).AddTicks(7040), new DateTime(2024, 4, 15, 1, 42, 48, 304, DateTimeKind.Local).AddTicks(7042) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 4, 15, 1, 42, 48, 304, DateTimeKind.Local).AddTicks(7060), new DateTime(2024, 4, 15, 1, 42, 48, 304, DateTimeKind.Local).AddTicks(7062) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 4, 15, 1, 42, 48, 304, DateTimeKind.Local).AddTicks(7079), new DateTime(2024, 4, 15, 1, 42, 48, 304, DateTimeKind.Local).AddTicks(7081) });
        }
    }
}
