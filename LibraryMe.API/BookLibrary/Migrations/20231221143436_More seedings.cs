using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Moreseedings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_VisitorsCards_ReadersCardId1",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "ReadersCardId",
                table: "Borrowings");

            migrationBuilder.RenameColumn(
                name: "ReadersCardId1",
                table: "Borrowings",
                newName: "VisitorsCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowings_ReadersCardId1",
                table: "Borrowings",
                newName: "IX_Borrowings_VisitorsCardId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VisitorsCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c608a4a7-14e7-4d19-bf4d-e17b22bfa097",
                column: "Name",
                value: "Reader");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "605e10f6-600d-4c5e-b983-4a235345053c", "AQAAAAIAAYagAAAAEHEj9Uh/XLIMSZnyh8YIIQNX48mXcFhadDw0fCcu2p+m92regDknw6T6MQKPNgBI1w==", "86954186-7934-4c93-9611-6321c296ca52" });

            migrationBuilder.InsertData(
                table: "BorrowingStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("73bb3243-c71c-4f1b-ba1f-f4fc56b5dee2"), "Active" },
                    { new Guid("76c30481-34b8-493e-857e-75622551a448"), "Returned" },
                    { new Guid("f037329e-b42c-456a-bf8f-b79cbc786433"), "Expired" }
                });

            migrationBuilder.InsertData(
                table: "ReservationStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5b0b6de5-7db3-4fb1-9173-8a1f4c2ff9c9"), "Declined" },
                    { new Guid("70b5342f-f380-47cf-b9d1-5e3f42a15ff0"), "Accepted" },
                    { new Guid("865a254e-5f32-44b1-aa2f-add87443bfb0"), "Expired" },
                    { new Guid("929b2083-c7b5-4d8c-b216-f02b0dc65af7"), "Processing" },
                    { new Guid("cc7951bd-8930-48c0-b7ce-aa60274c610e"), "Checked out" }
                });

            migrationBuilder.InsertData(
                table: "VisitorMemberships",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("23ee4dae-2a8a-4901-a048-78e21b42781e"), "Juvenile library" },
                    { new Guid("cc81e9d1-3f79-497c-8eaf-5da27afa871b"), "None" },
                    { new Guid("dfcdca9c-9858-416f-a49a-4843ed624e6c"), "Adolescent library" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_VisitorsCards_VisitorsCardId",
                table: "Borrowings",
                column: "VisitorsCardId",
                principalTable: "VisitorsCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_VisitorsCards_VisitorsCardId",
                table: "Borrowings");

            migrationBuilder.DeleteData(
                table: "BorrowingStatuses",
                keyColumn: "Id",
                keyValue: new Guid("73bb3243-c71c-4f1b-ba1f-f4fc56b5dee2"));

            migrationBuilder.DeleteData(
                table: "BorrowingStatuses",
                keyColumn: "Id",
                keyValue: new Guid("76c30481-34b8-493e-857e-75622551a448"));

            migrationBuilder.DeleteData(
                table: "BorrowingStatuses",
                keyColumn: "Id",
                keyValue: new Guid("f037329e-b42c-456a-bf8f-b79cbc786433"));

            migrationBuilder.DeleteData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: new Guid("5b0b6de5-7db3-4fb1-9173-8a1f4c2ff9c9"));

            migrationBuilder.DeleteData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: new Guid("70b5342f-f380-47cf-b9d1-5e3f42a15ff0"));

            migrationBuilder.DeleteData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: new Guid("865a254e-5f32-44b1-aa2f-add87443bfb0"));

            migrationBuilder.DeleteData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: new Guid("929b2083-c7b5-4d8c-b216-f02b0dc65af7"));

            migrationBuilder.DeleteData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: new Guid("cc7951bd-8930-48c0-b7ce-aa60274c610e"));

            migrationBuilder.DeleteData(
                table: "VisitorMemberships",
                keyColumn: "Id",
                keyValue: new Guid("23ee4dae-2a8a-4901-a048-78e21b42781e"));

            migrationBuilder.DeleteData(
                table: "VisitorMemberships",
                keyColumn: "Id",
                keyValue: new Guid("cc81e9d1-3f79-497c-8eaf-5da27afa871b"));

            migrationBuilder.DeleteData(
                table: "VisitorMemberships",
                keyColumn: "Id",
                keyValue: new Guid("dfcdca9c-9858-416f-a49a-4843ed624e6c"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VisitorsCards");

            migrationBuilder.RenameColumn(
                name: "VisitorsCardId",
                table: "Borrowings",
                newName: "ReadersCardId1");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowings_VisitorsCardId",
                table: "Borrowings",
                newName: "IX_Borrowings_ReadersCardId1");

            migrationBuilder.AddColumn<Guid>(
                name: "ReadersCardId",
                table: "Borrowings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c608a4a7-14e7-4d19-bf4d-e17b22bfa097",
                column: "Name",
                value: "Reder");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36fe1440-e481-4d9c-87e8-eda342e8e605", "AQAAAAIAAYagAAAAEMwbp7r3tdGATOl9tUzT8vSgSQrvXFwHO+uX1dd+RQrvFfux+ljvHBju4o8OXP41VA==", "a186fb21-a668-4fb6-8c3f-d477d0f940a4" });

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_VisitorsCards_ReadersCardId1",
                table: "Borrowings",
                column: "ReadersCardId1",
                principalTable: "VisitorsCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
