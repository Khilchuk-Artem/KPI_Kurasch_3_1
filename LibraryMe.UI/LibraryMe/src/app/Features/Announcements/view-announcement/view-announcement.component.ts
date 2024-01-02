import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {AnnouncementDTO} from "../models/announcement-dto";
import {AnnouncementsService} from "../services/announcements-service";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {AuthService} from "../../Auth/services/auth-service";

@Component({
  selector: 'app-view-announcement',
  standalone: true,
  imports: [
    DatePipe,
    NgIf,
    RouterLink,
    NgForOf
  ],
  templateUrl: './view-announcement.component.html',
  styleUrl: './view-announcement.component.css'
})
export class ViewAnnouncementComponent implements OnInit {
  announcementId=this.route.snapshot.params['id'];
  announcement!: AnnouncementDTO;

  constructor(private announcementService: AnnouncementsService,
              private route: ActivatedRoute,
              private authService:AuthService) {}

  ngOnInit(): void {
    if (this.announcementId) {
      this.loadAnnouncement();
    } else {
      // If you're using the component without providing an announcementId, handle it accordingly
    }
  }
  isLibarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
  loadAnnouncement(): void {
    // Assuming you have a service method like getAnnouncementById
    this.announcementService.getAnnouncementById(this.announcementId).subscribe(
        (announcement) => {
          this.announcement = announcement;
        },
        (error) => {
          console.error('Error loading announcement:', error);
          // Handle error, e.g., display an error message
        }
    );
  }
}
