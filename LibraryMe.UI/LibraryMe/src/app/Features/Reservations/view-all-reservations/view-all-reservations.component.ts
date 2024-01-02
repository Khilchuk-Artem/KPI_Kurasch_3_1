import {Component, OnInit} from '@angular/core';
import {DatePipe, NgClass, NgForOf} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ReservationSummaryDTO} from "../Models/reservation-summary-dto";
import {ReservationsService} from "../Services/reservations-service";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-view-all-reservations',
  standalone: true,
    imports: [
        NgForOf,
        ReactiveFormsModule,
        FormsModule,
        NgClass,
        RouterLink,
        DatePipe
    ],
  templateUrl: './view-all-reservations.component.html',
  styleUrl: './view-all-reservations.component.css'
})
export class ViewAllReservationsComponent implements OnInit {
    reservations: ReservationSummaryDTO[] = [];
    selectedQuery: string = '';
    pageSize: number = 10;
    pageNumber: number = 1;

    constructor(private reservationsService: ReservationsService) {}

    ngOnInit(): void {
        this.loadReservations();
    }

    loadReservations(): void {
        this.reservationsService.getReservationSummaries(this.pageSize, this.pageNumber)
            .subscribe((response) => {
                this.reservations = response;
            });
    }

    onInputChange(event: any) {
        // Remove non-numeric characters using a regular expression
        this.selectedQuery = event.target.value.replace(/[^0-9]/g, '');
    }
    onSearch(): void {
        this.reservationsService.getReservationSummaries(this.pageSize, this.pageNumber, this.selectedQuery)
            .subscribe((response) => {
                this.reservations = response;
            });
    }

    loadMore(): void {
        this.pageNumber++;
        this.reservationsService.getReservationSummaries(this.pageSize, this.pageNumber)
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
            default:
                return 'green-box';
        }
    }
}
