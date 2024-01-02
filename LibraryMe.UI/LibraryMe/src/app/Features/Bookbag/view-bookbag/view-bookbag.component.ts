import {AfterViewInit, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {Router, RouterLink} from "@angular/router";
import {BookbagService} from "../services/bookbag-service";
import {BookShortcutDTO} from "../../Books/models/book-shortcut-dto";
import {ReservationsService} from "../../Reservations/Services/reservations-service";
import {AuthService} from "../../Auth/services/auth-service";

@Component({
  selector: 'app-view-bookbag',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    RouterLink,
    DatePipe
  ],
  templateUrl: './view-bookbag.component.html',
  styleUrl: './view-bookbag.component.css'
})
export class ViewBookbagComponent implements OnInit, AfterViewInit{
  constructor(private bookbagService:BookbagService,
              private reservationsService:ReservationsService,
              private authService:AuthService,
              private router:Router,
              private cdRef: ChangeDetectorRef) {

  }
  books:BookShortcutDTO[]|null=null;

  hasBooks(){
    return this.books?.length!==0
  }
ngOnInit() {
  this.books=this.bookbagService.getBookbag()
  }
  currDate:Date=this.getCurrentDate()
  onRemove(shortcut:BookShortcutDTO) {
    this.bookbagService.removeItemFromBookBag(shortcut)
    this.books=this.bookbagService.getBookbag()
  }
  getCurrentDate(){

    return new Date()
  }
  getEstimDate(){
    var estimDate=this.getCurrentDate()
    return estimDate.setDate(estimDate.getDate()+14)
  }
  ngAfterViewInit() {
    // Perform any changes to component properties here
    this.cdRef.detectChanges();
  }
  onReserve(){
    var user = this.authService.getUser()
    if(user&&this.books&&this.books.length>0){
      this.reservationsService.createReservation({
            reservatorId:user.visitorsCardId,
          bookIds:this.books.map(b=>b.bookId)}).subscribe((response)=>{
            this.bookbagService.clearBookbag()
            this.router.navigate(['/reservations']);

      })
    }
  }
}
