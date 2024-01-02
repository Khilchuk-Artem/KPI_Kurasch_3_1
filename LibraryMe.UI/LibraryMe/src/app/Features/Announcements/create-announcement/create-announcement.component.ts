import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AnnouncementsService} from "../services/announcements-service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-announcement',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './create-announcement.component.html',
  styleUrl: './create-announcement.component.css'
})
export class CreateAnnouncementComponent implements OnInit {
  announcementForm!: FormGroup;

  constructor(
      private fb: FormBuilder,
      private announcementsService: AnnouncementsService,
      private router:Router
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.announcementForm = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.announcementForm.valid) {
      const announcementDTO = this.announcementForm.value;
      this.announcementsService.createAnnouncement(announcementDTO).subscribe(
          (response) => {
            this.router.navigate(['/announcements'])
            console.log('Announcement created successfully:', response);
            // Optionally, navigate to another page or perform additional actions
          },
          (error) => {
            console.error('Error creating announcement:', error);
            // Handle error, e.g., display an error message
          }
      );
    } else {
      console.log('Form is invalid.');
    }
  }
}
