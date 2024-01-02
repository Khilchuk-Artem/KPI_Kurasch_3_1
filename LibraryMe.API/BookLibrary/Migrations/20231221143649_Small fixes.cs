using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Smallfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "762f91b4-8bf8-4577-91cc-5563167b2ed6", "AQAAAAIAAYagAAAAEDTD/zDvzMAIkOUGbm54r67ULcMICJJNVIgvfJQgyRv4jOMXHc3rbFUQHvUFNyu10g==", "4d94dfa2-cb56-4c0c-9ae5-4f5499a11b30" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "605e10f6-600d-4c5e-b983-4a235345053c", "AQAAAAIAAYagAAAAEHEj9Uh/XLIMSZnyh8YIIQNX48mXcFhadDw0fCcu2p+m92regDknw6T6MQKPNgBI1w==", "86954186-7934-4c93-9611-6321c296ca52" });
        }
    }
}
