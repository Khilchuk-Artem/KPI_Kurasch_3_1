import {Component, OnInit} from '@angular/core';
import {NgForOf} from "@angular/common";
import {VisitorsCardsService} from "../services/visitor-cards-service";
import {VisitorCardShortcutDTO} from "../models/visitor-card-shortcut-dto";
import {RouterLink} from "@angular/router";
import {load} from "@angular-devkit/build-angular/src/utils/server-rendering/esm-in-memory-loader/loader-hooks";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-view-visitor-cards',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './view-visitor-cards.component.html',
  styleUrl: './view-visitor-cards.component.css'
})
export class ViewVisitorCardsComponent  implements OnInit{
  constructor(private visitorCardsService:VisitorsCardsService) {
  }
  pagesize=20
  pagenumber=1;
  cards!:VisitorCardShortcutDTO[]
  searchQuery=""
  ngOnInit() {
    this.visitorCardsService.getVisitorCardShortcuts().subscribe((response)=>{
      this.cards=response;
    })
  }
  loadMore(){
  this.pagenumber++;
    this.visitorCardsService.getVisitorCardShortcuts(this.pagesize,this.pagenumber).subscribe((response)=>{
      this.cards=[...this.cards,...response]
    })
  }
  updateCards(){
    this.pagenumber=1;
    this.visitorCardsService.getVisitorCardShortcuts(this.pagesize,this.pagenumber,this.searchQuery).subscribe((response)=>{
      this.cards=response;
    })
  }
}
