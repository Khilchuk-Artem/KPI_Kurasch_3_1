import {Component, OnInit} from '@angular/core';
import {BorrowingDTO} from "../models/borrowing-dto";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {BorrowingsService} from "../services/borrowings-service";
import {DatePipe, NgClass, NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-view-borrowing',
  standalone: true,
  imports: [
    NgClass,
    NgForOf,
    NgIf,
    RouterLink,
    DatePipe
  ],
  templateUrl: './view-borrowing.component.html',
  styleUrl: './view-borrowing.component.css'
})
export class ViewBorrowingComponent implements OnInit {
  borrowingId!: string;
  borrowing!: BorrowingDTO;

  constructor(
    private route: ActivatedRoute,
    private borrowingService: BorrowingsService,
  ) {}

  ngOnInit() {
    this.borrowingId = this.route.snapshot.params['id'];
    this.getBorrowingDetails();
  }

  getBorrowingDetails() {
    this.borrowingService
      .getBorrowingById(this.borrowingId)
      .subscribe((borrowing) => {
        this.borrowing = borrowing;
        console.log(borrowing)
      });
  }

  markAsReturned() {
    this.borrowingService.returnBorrowing(this.borrowingId).subscribe(() => {
      // Handle success, e.g., reload borrowings or perform any other action
      this.borrowing.status="Returned"
      console.log('Borrowing returned successfully');

    }, (error) => {
      // Handle error, e.g., display an error message
      console.error('Error returning borrowing:', error);
    });
  }
  getBoxClass(status: string): string {
    switch (status.toLowerCase()) {
      case 'expired':
        return 'red-box';
      case 'active':
        return 'blue-box';
      default:
        return 'green-box';
    }
  }
}
