import {Component, forwardRef, Inject, OnInit} from '@angular/core';
import { ImageService } from "../../../Shared/image.service";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthorService} from "../services/author.service";
import {Router} from "@angular/router";
import {CreateAuthorDTO} from "../models/create-author-dto";

@Component({
  selector: 'app-create-author',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './create-author.component.html',
  styleUrls: ['./create-author.component.css']
})
export class CreateAuthorComponent implements OnInit{
  authorForm!: FormGroup;
  imageId!: string;

  constructor(
    private fb: FormBuilder,
    private imageService: ImageService,
    private authorService: AuthorService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authorForm = this.fb.group({
      file: [null, Validators.required],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      patronymic: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      dateOfDeath: [''],
      biography: ['', Validators.required]
    });
  }
  onSubmit(): void {
    if (this.authorForm.valid) {
      const formData = this.authorForm.value as CreateAuthorDTO;
      formData.imageId = this.imageId.toString(); // Assuming imageId is a string

      this.authorService.createAuthor(formData).subscribe(
        (createdAuthor) => {
          console.log('Author created successfully:', createdAuthor);
          //this.router.navigate(['/authors', createdAuthor.id]);
          this.router.navigate(['/authors']);
        },
        (error) => {
          console.error('Error creating author:', error);
          // Handle error
        }
      );
    }
  }
  onFileSelected(event: any): void {
    const file: File = event.target.files[0];

    if (file) {
      this.imageService.uploadImage(file).subscribe(
        (imageId: string) => {
          console.log('Image uploaded successfully. Image ID:', imageId);
          this.imageId=imageId
        },
        (error) => {
          console.error('Error uploading image:', error);
        }
      );
    }
  }
}
