import {Component, OnInit} from '@angular/core';
import {NgClass, NgForOf} from "@angular/common";
import {BorrowingSummaryDTO} from "../models/borrowing-summary-dto";
import {BorrowingsService} from "../services/borrowings-service";
import {AuthService} from "../../Auth/services/auth-service";
import {RouterLink} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-view-all-borrowings',
  standalone: true,
    imports: [
        NgForOf,
        NgClass,
        RouterLink,
        ReactiveFormsModule,
        FormsModule
    ],
  templateUrl: './view-all-borrowings.component.html',
  styleUrl: './view-all-borrowings.component.css'
})
export class ViewAllBorrowingsComponent implements OnInit{
  ngOnInit() {
    console.log("fetching")
    this.borrowingService.getBorrowingSummaries(10,1).subscribe((response)=>{
      this.borrowings=response;
    })

    console.log("finished")
  }
  borrowings!:BorrowingSummaryDTO[];
  constructor(private borrowingService:BorrowingsService,
              private authService:AuthService,) {

  }
  // Property to store the state of the checkbox
  hideReturned: boolean = false;
  selectedStartDate: Date | null = null;
  selectedEndDate: Date | null = null;
  selectedQuery: string |null=null;
  pageNumber:number=1;
  pageSize:number=10;
  // Method to handle checkbox changes
  handleCheckboxChange() {
    this.hideReturned=!this.hideReturned;
    console.log('Option 1 Checked:', this.hideReturned);
    // Add your logic here based on the checkbox state
      this.onCriterionsChanged()
  }
    onStartDateChange(event: any) {
        // Access the selected date
        this.selectedStartDate = event.target.value;

        // Handle the change here or call a method
        console.log('Selected Date:', this.selectedStartDate);
        this.onCriterionsChanged()

    }
    onEndDateChange(event: any) {
        // Access the selected date
        this.selectedEndDate = event.target.value;

        // Handle the change here or call a method
        console.log('Selected Date:', this.selectedEndDate);
        this.onCriterionsChanged()

    }
    onSearchChange(){

    }
    onSearch(){
        this.onCriterionsChanged()
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
    onCriterionsChanged(){
    this.pageNumber=1;
        this.borrowingService.getBorrowingSummaries(10,1,null,
            this.selectedQuery,this.hideReturned,this.selectedStartDate,this.selectedEndDate).subscribe((response)=>{
            this.borrowings=response;
        })
    }
    loadMore(){
        this.pageNumber++;
        this.borrowingService
            .getBorrowingSummaries(this.pageSize,this.pageNumber, null, this.selectedQuery, this.hideReturned, this.selectedStartDate, this.selectedEndDate)
            .subscribe((response) => {
                // Append or replace the data based on the appendData flag
                this.borrowings = [...this.borrowings, ...response];
                console.log(response)
            });
    }
}
