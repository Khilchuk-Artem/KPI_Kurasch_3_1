using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Addedtherest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Borrowings_BorrowingId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Reservations_ReservationId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowingId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ReservationId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BorrowingId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "BookBorrowing",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BorrowingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowing", x => new { x.BooksId, x.BorrowingsId });
                    table.ForeignKey(
                        name: "FK_BookBorrowing_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBorrowing_Borrowings_BorrowingsId",
                        column: x => x.BorrowingsId,
                        principalTable: "Borrowings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookReservation",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReservation", x => new { x.BooksId, x.ReservationsId });
                    table.ForeignKey(
                        name: "FK_BookReservation_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookReservation_Reservations_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowing_BorrowingsId",
                table: "BookBorrowing",
                column: "BorrowingsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservation_ReservationsId",
                table: "BookReservation",
                column: "ReservationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBorrowing");

            migrationBuilder.DropTable(
                name: "BookReservation");

            migrationBuilder.AddColumn<Guid>(
                name: "BorrowingId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowingId",
                table: "Books",
                column: "BorrowingId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ReservationId",
                table: "Books",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Borrowings_BorrowingId",
                table: "Books",
                column: "BorrowingId",
                principalTable: "Borrowings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Reservations_ReservationId",
                table: "Books",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
