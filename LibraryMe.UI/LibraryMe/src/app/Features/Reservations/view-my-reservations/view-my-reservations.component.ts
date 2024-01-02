import {Component, OnInit} from '@angular/core';
import {DatePipe, NgClass, NgForOf} from "@angular/common";
import {ReactiveFormsModule} from "@angular/forms";
import {ReservationSummaryDTO} from "../Models/reservation-summary-dto";
import {ReservationsService} from "../Services/reservations-service";
import {AuthService} from "../../Auth/services/auth-service";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-view-my-reservations',
  standalone: true,
  imports: [
    DatePipe,
    NgForOf,
    ReactiveFormsModule,
    NgClass,
    RouterLink
  ],
  templateUrl: './view-my-reservations.component.html',
  styleUrl: './view-my-reservations.component.css'
})
export class ViewMyReservationsComponent implements OnInit {
  reservations: ReservationSummaryDTO[] = [];
  pageSize: number = 10;
  pageNumber: number = 1;
  userCardId:string|undefined;
  constructor(private reservationsService: ReservationsService,
              private authService:AuthService) {}

  ngOnInit(): void {
    this.userCardId = this.authService.getUser()?.visitorsCardId
    this.loadReservations();
  }

  loadReservations(): void {
    if(this.userCardId){
      this.reservationsService.getReservationSummaries(this.pageSize, this.pageNumber,"",this.userCardId)
          .subscribe((response) => {
            this.reservations = response;
          });
    }
  }


  loadMore(): void {
    this.pageNumber++;
    this.reservationsService.getReservationSummaries(this.pageSize, this.pageNumber,"",this.userCardId)
        .subscribe((response) => {
          this.reservations = [...this.reservations, ...response];
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
}
