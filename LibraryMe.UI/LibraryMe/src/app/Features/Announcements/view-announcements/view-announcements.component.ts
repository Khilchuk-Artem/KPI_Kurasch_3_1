import {Component, OnInit} from '@angular/core';
import {AnnouncementDTO} from "../models/announcement-dto";
import {AnnouncementsService} from "../services/announcements-service";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {RouterLink} from "@angular/router";
import {AuthService} from "../../Auth/services/auth-service";

@Component({
  selector: 'app-view-announcements',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink,
    NgForOf,
    NgIf
  ],
  templateUrl: './view-announcements.component.html',
  styleUrl: './view-announcements.component.css'
})
export class ViewAnnouncementsComponent implements OnInit {
  announcements!: AnnouncementDTO[];
  pageSize=3;
  pageNumber=1;
  isLoading=false;
  hasNext=true;
  holdOnMsg="Loading..."
  constructor(private announcementsService: AnnouncementsService,
              private authService:AuthService) {}

  isLibarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
  ngOnInit(): void {
    this.loadAnnouncements();
  }

  loadAnnouncements(): void {
    this.isLoading=true;
    this.announcementsService.getAnnouncementSummaries(this.pageSize,this.pageNumber).subscribe(
        (announcements) => {
          console.log(announcements)
          this.announcements = announcements;
          this.isLoading=false;
          this.holdOnMsg="No announcements found"
        },
        (error) => {
          console.error('Error loading announcements:', error);
          // Handle error, e.g., display an error message
          this.isLoading=false;

        }
    );
  }
  loadMore(){
    this.pageNumber++;
    this.isLoading=true;
    this.announcementsService.getAnnouncementSummaries(this.pageSize,this.pageNumber).subscribe(
      (announcements) => {
        this.announcements = [...this.announcements,...announcements];
        this.isLoading=false;
        if(announcements.length===0) this.hasNext=false;
      },
      (error) => {
        console.error('Error loading announcements:', error);
        this.isLoading=false;
      }
    );
  }
}
