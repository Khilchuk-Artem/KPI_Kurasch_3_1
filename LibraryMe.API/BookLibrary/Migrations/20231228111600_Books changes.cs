using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Bookschanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableAmount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af954a7e-140a-49c8-b60f-e9928fdad54c", "AQAAAAIAAYagAAAAEEmncVyCiHzu1kjiPg/RLB2Su+aqWmuM4XUZ61yziUzhfofHzpKPtvnfx+o5bt/ARQ==", "48956776-33b6-4a76-9a8a-3c1576794ca5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableAmount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b26039d-5a27-45a3-a8a4-37b75469c2c8", "AQAAAAIAAYagAAAAEJjlHPgdPOxwpOl6a1P/WzfAsjYifiZ2QbzvlUe/L7uVAyZtNFVJ6+ec4IWOBZ2luQ==", "b6116c0f-e87e-46eb-9a48-79b6be03b416" });
        }
    }
}
