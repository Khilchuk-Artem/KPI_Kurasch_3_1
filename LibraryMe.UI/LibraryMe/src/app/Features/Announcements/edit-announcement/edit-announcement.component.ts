import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AnnouncementsService} from "../services/announcements-service";
import {ActivatedRoute, Route, Router} from "@angular/router";

@Component({
  selector: 'app-edit-announcement',
  standalone: true,
    imports: [
        ReactiveFormsModule
    ],
  templateUrl: './edit-announcement.component.html',
  styleUrl: './edit-announcement.component.css'
})
export class EditAnnouncementComponent implements OnInit {
  announcementForm!: FormGroup;
  announcementId=this.route.snapshot.params['id'];

  constructor(
      private fb: FormBuilder,
      private announcementsService: AnnouncementsService,
      private route:ActivatedRoute,
      private router:Router
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.announcementsService.getAnnouncementById(this.announcementId).subscribe((response)=>{
      this.announcementForm.patchValue({
        title:response.title,
        content:response.content
      })
    })
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
      this.announcementsService.updateAnnouncement(this.announcementId,announcementDTO).subscribe(
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
