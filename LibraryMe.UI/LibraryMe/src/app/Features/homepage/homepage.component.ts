import {Component, OnInit} from '@angular/core';
import {AnnouncementDTO} from "../Announcements/models/announcement-dto";
import {AnnouncementsService} from "../Announcements/services/announcements-service";
import {DatePipe, NgForOf} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink,
    NgForOf
  ],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent implements OnInit {
  announcements!: AnnouncementDTO[];

  constructor(private announcementsService: AnnouncementsService) {
  }

  ngOnInit(): void {
    this.loadAnnouncements();
  }

  loadAnnouncements(): void {
    this.announcementsService.getAnnouncementSummaries(1,1).subscribe(
      (announcements) => {
        this.announcements = announcements;
      },
      (error) => {
        console.error('Error loading announcements:', error);
        // Handle error, e.g., display an error message
      }
    );
  }
}
