using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemovedobsoletestufffromVisitorCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitorsCards_VisitorMemberships_VisitorMembershipId",
                table: "VisitorsCards");

            migrationBuilder.DropIndex(
                name: "IX_VisitorsCards_VisitorMembershipId",
                table: "VisitorsCards");

            migrationBuilder.DropColumn(
                name: "VisitorAccountId",
                table: "VisitorsCards");

            migrationBuilder.DropColumn(
                name: "VisitorMembershipId",
                table: "VisitorsCards");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bb43622-446d-4cb1-bd44-b2a135ed58dc", "AQAAAAIAAYagAAAAEDcIgiFN3TiI6TZvwz5wgMTr8+URT2z5CltZsD6xNoNMatjOGYAU5Le5Mo1KlgfTHg==", "83c5f66a-4225-4ecc-955f-cabfe1f8d64f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VisitorAccountId",
                table: "VisitorsCards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitorMembershipId",
                table: "VisitorsCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1095f014-baf1-4b28-a7ad-318a9cb2b898", "AQAAAAIAAYagAAAAEKQ5a8Oz45e64yqZOp9qNiuPjiL/wQYkDvb779nhMfdM63Npvq026K3+rU2bSwqdYQ==", "dcb7fe1e-30b9-426d-a967-3961a0d969b8" });

            migrationBuilder.CreateIndex(
                name: "IX_VisitorsCards_VisitorMembershipId",
                table: "VisitorsCards",
                column: "VisitorMembershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorsCards_VisitorMemberships_VisitorMembershipId",
                table: "VisitorsCards",
                column: "VisitorMembershipId",
                principalTable: "VisitorMemberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
