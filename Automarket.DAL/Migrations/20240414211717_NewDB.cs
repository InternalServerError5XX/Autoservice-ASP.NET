using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Automarket.DAL.Migrations
{
    public partial class NewDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallBack = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Checked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consumables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumableType = table.Column<int>(type: "int", nullable: false),
                    Subcategory = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: false),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "CreationDate", "Email", "LastLogin", "Lastname", "Name", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1L, 20, new DateTime(2024, 4, 15, 0, 17, 17, 207, DateTimeKind.Local).AddTicks(6433), "secauto.admin@gmail.com", new DateTime(2024, 4, 15, 0, 17, 17, 207, DateTimeKind.Local).AddTicks(6467), "Linnik", "Vlad", "80e0cae0c86fc08fce7b5c03dd1229078862ab996042db9f4299bd3e838cda2a", 0, "admin" },
                    { 2L, 20, new DateTime(2024, 4, 15, 0, 17, 17, 207, DateTimeKind.Local).AddTicks(6491), "secauto.administrator@gmail.com", new DateTime(2024, 4, 15, 0, 17, 17, 207, DateTimeKind.Local).AddTicks(6493), "Hranoskyi", "Dimasik", "f301d9014ab4016549a8bf0947644c81f971f79ffa555eb373a3232c71c8f08b", 1, "administrator" },
                    { 3L, 20, new DateTime(2024, 4, 15, 0, 17, 17, 207, DateTimeKind.Local).AddTicks(6511), "secauto.mechanic@gmail.com", new DateTime(2024, 4, 15, 0, 17, 17, 207, DateTimeKind.Local).AddTicks(6513), "Ishchuk", "Andriy", "7d7401591c213d25d2ca8d65542ca054ecd2fb7ee746094cf4c38acfb42cb744", 2, "mechanic" },
                    { 4L, 20, new DateTime(2024, 4, 15, 0, 17, 17, 207, DateTimeKind.Local).AddTicks(6533), "secauto.testuser@gmail.com", new DateTime(2024, 4, 15, 0, 17, 17, 207, DateTimeKind.Local).AddTicks(6534), "User", "Test", "4ace4f0a32ae2bcfece96c5b17c8f74f75c7e2c07a2ce7c6e0deba4553e10f90", 3, "TestUser" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Consumables");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
