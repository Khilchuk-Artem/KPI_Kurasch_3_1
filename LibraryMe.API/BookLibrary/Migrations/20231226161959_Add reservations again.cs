using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Addreservationsagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ReservatorId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b26039d-5a27-45a3-a8a4-37b75469c2c8", "AQAAAAIAAYagAAAAEJjlHPgdPOxwpOl6a1P/WzfAsjYifiZ2QbzvlUe/L7uVAyZtNFVJ6+ec4IWOBZ2luQ==", "b6116c0f-e87e-46eb-9a48-79b6be03b416" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservatorId",
                table: "Reservations",
                column: "ReservatorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_VisitorsCards_ReservatorId",
                table: "Reservations",
                column: "ReservatorId",
                principalTable: "VisitorsCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservation_Reservations_ReservationsId",
                table: "BookReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_VisitorsCards_ReservatorId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservatorId",
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
    }
}
