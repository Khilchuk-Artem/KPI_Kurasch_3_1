import {Component, OnInit} from '@angular/core';
import {UserSummaryService} from "../services/user-summary-service";
import {UserSummaryShortcutDTO} from "../models/user-summary-shortcut";
import {NgForOf} from "@angular/common";
import {RouterLink} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-view-profiles',
  standalone: true,
    imports: [
        NgForOf,
        RouterLink,
        ReactiveFormsModule,
        FormsModule
    ],
  templateUrl: './view-profiles.component.html',
  styleUrl: './view-profiles.component.css'
})
export class ViewProfilesComponent implements OnInit{
  constructor(private userSummaryService:UserSummaryService) {
  }
  users!:UserSummaryShortcutDTO[]
  pagesize=20;
  pageNumber=1;
  selectedQuery=""
  ngOnInit() {
    this.userSummaryService.getUserSummaryShortcuts().subscribe((response)=>{
      this.users=response;
    })
  }
  loadMore(){
    this.pageNumber++;
      this.userSummaryService.getUserSummaryShortcuts(this.pagesize,this.pageNumber,this.selectedQuery).subscribe((response)=>{
          this.users=[...this.users,...response]
      })
  }
  updateUsers(){
      this.pageNumber=1;
      this.userSummaryService.getUserSummaryShortcuts(this.pagesize,this.pageNumber,this.selectedQuery).subscribe((response)=>{
          this.users=response;
      })
  }
}
