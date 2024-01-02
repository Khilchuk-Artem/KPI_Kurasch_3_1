import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {ImageService} from "../../../Shared/image.service";
import {AuthorService} from "../services/author.service";
import {ActivatedRoute, Router} from "@angular/router";
import {CreateAuthorDTO} from "../models/create-author-dto";
import {AuthorDTO} from "../models/author-dto";

@Component({
  selector: 'app-edit-author',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './edit-author.component.html',
  styleUrl: './edit-author.component.css'
})
export class EditAuthorComponent implements OnInit{
  authorForm: FormGroup=this.fb.group({
    file: [null], // You may want to add validation if needed
    name: ["", Validators.required],
    surname: ["", Validators.required],
    patronymic: ["", Validators.required],
    dateOfBirth: ["", Validators.required],
    dateOfDeath: [""],
    biography: ["", Validators.required]
  });
  imageId!: string;

  constructor(
    private fb: FormBuilder,
    private imageService: ImageService,
    private authorService: AuthorService,
    private router: Router,
    private activatedRoute:ActivatedRoute
  ) {}
  ngOnInit() {
    const authorId = this.activatedRoute.snapshot.params['id'];

    this.authorService.getAuthorById(authorId).subscribe(
      (author: AuthorDTO) => {
        //this.authorData = author;

        // Initialize the form with fetched data
        this.authorForm = this.fb.group({
          file: [null],
          name: [author.name, Validators.required],
          surname: [author.surname, Validators.required],
          patronymic: [author.patronymic, Validators.required],
          dateOfBirth: [author.dateOfBirth, Validators.required],
          dateOfDeath: [author.dateOfDeath],
          biography: [author.biography, Validators.required]
        });

        this.imageService.getImageIdByUrl(author.imageUrl).subscribe((imageid:string)=>{
            this.imageId = imageid;
            console.log(this.imageId)
        }, (error)=>{
            console.error('Error fetching imageId:', error);
        });
      },
      (error) => {
        console.error('Error fetching author data:', error);
      }
    );
  }
  onSubmit(): void {
    if (this.authorForm.valid) {
      const formData = this.authorForm.value as CreateAuthorDTO;
      formData.imageId = this.imageId.toString(); // Assuming imageId is a string

      this.authorService.updateAuthor(this.activatedRoute.snapshot.params['id'],formData).subscribe(
        (createdAuthor) => {
          console.log('Author created successfully:', createdAuthor);
          this.router.navigate(['/authors', this.activatedRoute.snapshot.params['id']]);
        },
        (error) => {
          console.error('Error creating author:', error);
        }
      );
    }
  }
  onDelete(){
    this.authorService.deleteAuthor(this.activatedRoute.snapshot.params['id']).subscribe(()=>{
      this.router.navigate(['/authors'])
    })
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
