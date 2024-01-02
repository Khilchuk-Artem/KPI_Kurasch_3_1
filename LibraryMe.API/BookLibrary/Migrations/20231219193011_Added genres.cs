using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Addedgenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableAMount",
                table: "Books",
                newName: "AvailableAmount");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0810f87d-0327-41ad-8206-89edac981813"), "Action" },
                    { new Guid("2b81926c-b152-4b4f-9ae1-311eb99e4386"), "Fantasy" },
                    { new Guid("6a3cd06d-fa8d-4c06-90cb-c87ef2f39f16"), "Classic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0810f87d-0327-41ad-8206-89edac981813"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2b81926c-b152-4b4f-9ae1-311eb99e4386"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("6a3cd06d-fa8d-4c06-90cb-c87ef2f39f16"));

            migrationBuilder.RenameColumn(
                name: "AvailableAmount",
                table: "Books",
                newName: "AvailableAMount");
        }
    }
}
