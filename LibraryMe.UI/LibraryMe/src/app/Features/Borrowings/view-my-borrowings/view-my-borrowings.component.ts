import {Component, OnInit} from '@angular/core';
import {BorrowingsService} from "../services/borrowings-service";
import {AuthService} from "../../Auth/services/auth-service";
import {BorrowingSummaryDTO} from "../models/borrowing-summary-dto";
import {NgClass, NgForOf} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-view-my-borrowings',
  standalone: true,
  imports: [
    NgClass,
    NgForOf,
    RouterLink
  ],
  templateUrl: './view-my-borrowings.component.html',
  styleUrl: './view-my-borrowings.component.css'
})
export class ViewMyBorrowingsComponent implements OnInit{
  ngOnInit() {
    console.log("fetching")
    this.visitorCard=this.authService.getUser()?.visitorsCardId
    console.log(this.visitorCard)
    if(this.visitorCard){
      this.borrowingService.getBorrowingSummaries(10,1,this.visitorCard).subscribe((response)=>{
        this.borrowings=response;
      })

    }
  }
  borrowings!:BorrowingSummaryDTO[];
  visitorCard!:string|undefined;
  constructor(private borrowingService:BorrowingsService,
              private authService:AuthService,) {

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
