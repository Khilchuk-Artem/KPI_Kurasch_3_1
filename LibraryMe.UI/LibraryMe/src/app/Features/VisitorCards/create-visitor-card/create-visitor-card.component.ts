import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {VisitorsCardsService} from "../services/visitor-cards-service";
import {CreateVisitorCardDTO} from "../models/create-visitor-card-dto";

@Component({
  selector: 'app-create-visitor-card',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './create-visitor-card.component.html',
  styleUrl: './create-visitor-card.component.css'
})
export class CreateVisitorCardComponent implements OnInit {
  visitorForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private visitorsCardsService: VisitorsCardsService
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
  }

  onSubmit(): void {
    if (this.visitorForm.valid) {
      const createVisitorDTO: CreateVisitorCardDTO = this.visitorForm.value;
      this.visitorsCardsService.createVisitorCard(createVisitorDTO).subscribe(
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
