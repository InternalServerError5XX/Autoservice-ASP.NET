using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Automarket.DAL.Migrations
{
    public partial class Basket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 5, 17, 23, 32, 21, 202, DateTimeKind.Local).AddTicks(7269), new DateTime(2024, 5, 17, 23, 32, 21, 202, DateTimeKind.Local).AddTicks(7305) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 5, 17, 23, 32, 21, 202, DateTimeKind.Local).AddTicks(7330), new DateTime(2024, 5, 17, 23, 32, 21, 202, DateTimeKind.Local).AddTicks(7332) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 5, 17, 23, 32, 21, 202, DateTimeKind.Local).AddTicks(7371), new DateTime(2024, 5, 17, 23, 32, 21, 202, DateTimeKind.Local).AddTicks(7373) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreationDate", "LastLogin" },
                values: new object[] { new DateTime(2024, 5, 17, 23, 32, 21, 202, DateTimeKind.Local).AddTicks(7391), new DateTime(2024, 5, 17, 23, 32, 21, 202, DateTimeKind.Local).AddTicks(7393) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basket");

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
    }
}
