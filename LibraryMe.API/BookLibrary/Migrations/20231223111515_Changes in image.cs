using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Changesinimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1095f014-baf1-4b28-a7ad-318a9cb2b898", "AQAAAAIAAYagAAAAEKQ5a8Oz45e64yqZOp9qNiuPjiL/wQYkDvb779nhMfdM63Npvq026K3+rU2bSwqdYQ==", "dcb7fe1e-30b9-426d-a967-3961a0d969b8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c595a8f-6aab-4dfd-ac37-2634d60bb186", "AQAAAAIAAYagAAAAENB7X7r7AWBQcjbnTAzp+tfsYNaGFiphYBQ7+h6ZEH6vRvxTPwad+HOwvriYeCCVfQ==", "e78832a2-8f41-492c-92f3-8cf500686440" });
        }
    }
}
