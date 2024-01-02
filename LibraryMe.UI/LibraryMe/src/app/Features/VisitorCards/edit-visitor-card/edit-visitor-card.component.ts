import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {VisitorsCardsService} from "../services/visitor-cards-service";
import {CreateVisitorCardDTO} from "../models/create-visitor-card-dto";
import {ActivatedRoute, RouterLink} from "@angular/router";

@Component({
  selector: 'app-edit-visitor-card',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './edit-visitor-card.component.html',
  styleUrl: './edit-visitor-card.component.css'
})
export class EditVisitorCardComponent implements OnInit {
  visitorForm!: FormGroup;
  id!:number;
  constructor(
    private fb: FormBuilder,
    private visitorsCardsService: VisitorsCardsService,
    private route:ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.visitorForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      patronymic: [''],
      phoneNumber: ['', Validators.required],
    });
    this.id=this.route.snapshot.params['id'];
    this.visitorsCardsService.getVisitorCardById(this.id).subscribe((reponse)=>{
      this.visitorForm.patchValue({
        name:reponse.name,
        surname:reponse.surname,
        patronymic:reponse.patronymic,
        phoneNumber:reponse.phoneNumber
      })
    })
  }

  onSubmit(): void {
    if (this.visitorForm.valid) {
      const createVisitorDTO: CreateVisitorCardDTO = this.visitorForm.value;
      this.visitorsCardsService.updateVisitorCard(this.id, createVisitorDTO).subscribe(
        (response) => {
          console.log(response)
          console.log('Visitor card created successfully:', response);
          // Optionally, navigate to another page or perform additional actions
        },
        (error) => {
          console.error('Error creating visitor card:', error);
          // Handle error, e.g., display an error message
        }
      );
    } else {
      console.log('Form is invalid.');
    }
  }
}
