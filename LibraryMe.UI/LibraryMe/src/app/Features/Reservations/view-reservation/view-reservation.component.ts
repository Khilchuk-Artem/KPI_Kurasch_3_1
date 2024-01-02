import {Component, OnInit} from '@angular/core';
import {DatePipe, NgClass, NgForOf, NgIf} from "@angular/common";
import {ReservationDTO} from "../Models/reservation-dto";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {ReservationsService} from "../Services/reservations-service";
import {BorrowingsService} from "../../Borrowings/services/borrowings-service";
import {AuthService} from "../../Auth/services/auth-service";

@Component({
  selector: 'app-view-reservation',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    DatePipe,
    RouterLink,
    NgClass
  ],
  templateUrl: './view-reservation.component.html',
  styleUrl: './view-reservation.component.css'
})
export class ViewReservationComponent implements OnInit {
  reservationId!: number;
  reservation!: ReservationDTO;

  constructor(
    private route: ActivatedRoute,
    private reservationsService: ReservationsService,
    private router: Router,
    private borrowingsService: BorrowingsService,
    private authService: AuthService
  ) {
  }

  ngOnInit(): void {
    this.reservationId = this.route.snapshot.params['id']


    // Fetch reservation details by ID
    this.reservationsService.getReservationById(this.reservationId)
      .subscribe(reservation => {
        this.reservation = reservation;
      });
  }

  getBoxClass(status: string): string {
    switch (status.toLowerCase()) {
      case 'expired':
        return 'red-box';
      case 'accepted':
        return 'blue-box';
      case 'processing':
        return 'yellow-box';
      case 'declined':
        return 'orange-box';
      default:
        return 'green-box';
    }
  }

  acceptReservation() {
    this.reservationsService.acceptReservation(this.reservationId).subscribe(() => {
      console.log("Accepted")
      this.reservation.status = "Accepted"
      this.router.navigate(['/reservations', this.reservationId])
    })
  }

  declineReservation() {
    this.reservationsService.declineReservation(this.reservationId).subscribe(() => {
      console.log("Declined")
      this.reservation.status = "Declined"

      this.router.navigate(['/reservations', this.reservationId])
    })
  }

  ckeckOutReservation() {
    var cardId = this.authService.getUser()?.visitorsCardId
    if (cardId) {
      this.reservationsService.checkOutReservation(this.reservationId).subscribe(() => {
        console.log("Checked out")
        this.reservation.status = "Checked out"
        if (cardId) {
          this.borrowingsService.createBorrowing({
            borrowerId: +cardId,
            bookIds: this.reservation.books.map(b => b.bookId)
          }).subscribe((id)=>{
            this.router.navigate(['/borrowings', id])
            console.log("Created,", id)
          })
        }
      })
    }

  }
  isLibrarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
}
