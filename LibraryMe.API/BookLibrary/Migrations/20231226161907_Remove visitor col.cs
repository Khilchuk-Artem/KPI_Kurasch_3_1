using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Removevisitorcol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservation_Reservations_ReservationsId",
                table: "BookReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservatorId",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ReservationStatusId",
                table: "Reservation",
                newName: "IX_Reservation_ReservationStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4ed3eeb-3d1e-44de-a8a3-a19838b0407e", "AQAAAAIAAYagAAAAEP9PkV/tsUPXYZtCDJn04r+AUpVxeWEZzyqV5ScxGYURibSOtBsWuZOAaZKu1AUmOg==", "cb1a0f16-bcbf-40b3-889e-d80e5a1a763e" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservation_Reservation_ReservationsId",
                table: "BookReservation",
                column: "ReservationsId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_ReservationStatuses_ReservationStatusId",
                table: "Reservation",
                column: "ReservationStatusId",
                principalTable: "ReservationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservation_Reservation_ReservationsId",
                table: "BookReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_ReservationStatuses_ReservationStatusId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_ReservationStatusId",
                table: "Reservations",
                newName: "IX_Reservations_ReservationStatusId");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservatorId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bb43622-446d-4cb1-bd44-b2a135ed58dc", "AQAAAAIAAYagAAAAEDcIgiFN3TiI6TZvwz5wgMTr8+URT2z5CltZsD6xNoNMatjOGYAU5Le5Mo1KlgfTHg==", "83c5f66a-4225-4ecc-955f-cabfe1f8d64f" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservation_Reservations_ReservationsId",
                table: "BookReservation",
                column: "ReservationsId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations",
                column: "ReservationStatusId",
                principalTable: "ReservationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
