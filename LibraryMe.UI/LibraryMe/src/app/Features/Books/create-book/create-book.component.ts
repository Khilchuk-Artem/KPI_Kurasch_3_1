import { Component, ViewChild } from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import { ImageService } from "../../../Shared/image.service";
import { AuthorService } from "../../Authors/services/author.service";
import { Router } from "@angular/router";
import { CreateAuthorDTO } from "../../Authors/models/create-author-dto";
import {NgSelectComponent, NgSelectModule} from "@ng-select/ng-select";
import { Observable } from "rxjs";
import { GenreDTO } from "../Genres/models/genre-dto";
import { GenreService } from "../Genres/services/genre-service";
import { AuthorLinkDTO } from "../../Authors/models/author-link-dto";
import { BookService } from "../services/book-service";
import {AsyncPipe, NgForOf} from "@angular/common";
import {CreateBookDTO} from "../models/create-book-dto";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-create-book',
  standalone: true,
  templateUrl: './create-book.component.html',
  imports: [
    ReactiveFormsModule,
    NgSelectModule,
    FormsModule,
    AsyncPipe,
    NgForOf
  ],
  styleUrl: './create-book.component.css'
})
export class CreateBookComponent {
  bookForm!: FormGroup;
  imageId!: string;
  genres!: Observable<GenreDTO[]>
  authors!: Observable<AuthorLinkDTO[]>
  @ViewChild("genreSelect") genreSelect!: NgSelectComponent;
  @ViewChild("authorSelect") authorSelect!: NgSelectComponent;
  selectedGenre!: GenreDTO;
  selectedAuthorLink!:AuthorLinkDTO;
  selectedGenres: GenreDTO[] = [];
  selectedAuthors: AuthorLinkDTO[] = [];

  constructor(
    private fb: FormBuilder,
    private imageService: ImageService,
    private authorService: AuthorService,
    private bookService: BookService,
    private router: Router,
    private genreService: GenreService
  ) {}

  ngOnInit() {
    this.bookForm = this.fb.group({
      file: [null, Validators.required],
      title: ['', Validators.required], // Assuming 'title' is the name for the book
      description: [''],
    });
    this.genres = this.genreService.getGenres();
    this.authors = this.authorService.getAuthorLinks();
  }

  onSubmit(): void {
    if (this.bookForm.valid) {
      const formData = this.bookForm.value as CreateBookDTO;


      formData.imageId = this.imageId.toString(); // Assuming imageId is a string
      formData.genreIds = this.selectedGenres.map(genre => genre.id);
      formData.authorIds = this.selectedAuthors.map(author => author.authorId);
      console.log(formData)
      this.bookService.createBook(formData).subscribe(
        (createdBook) => {
          console.log('Book created successfully:', createdBook);
          this.router.navigate(['/books']);
        },
        (error) => {
          console.error('Error creating book:', error);

          if (error instanceof HttpErrorResponse && error.status === 400) {
            // Handle validation errors
            const validationErrors = error.error.errors;

            // Example: Log validation errors
            console.error('Validation errors:', validationErrors);

            // You can also display error messages to the user or take other appropriate actions
          } else {
            // Handle other types of errors
            // Example: Display a generic error message
            console.error('An unexpected error occurred. Please try again.');
          }
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
          this.imageId = imageId;
        },
        (error) => {
          console.error('Error uploading image:', error);
        }
      );
    }
  }

  changeGenre(event: any): void {
    if (this.selectedGenres.findIndex(item => item.id === event.id) === -1) {
      this.selectedGenres.push({ id: event.id, name: event.name });
    }
    setTimeout(() => {
      this.genreSelect.handleClearClick();
    });
  }

  removeGenre(genre: GenreDTO): void {
    const index = this.selectedGenres.findIndex(item => item.id === genre.id);

    if (index !== -1) {
      this.selectedGenres.splice(index, 1);
    }
  }

  changeAuthorLink(event: any): void {
    if (this.selectedAuthors.findIndex(item => item.authorId === event.authorId) === -1) {
      this.selectedAuthors.push({ authorId: event.authorId, name: event.name });
    }
    setTimeout(() => {
      this.authorSelect.handleClearClick();
    });
    console.log(this.selectedAuthors)
  }

  removeAuthor(author: AuthorLinkDTO): void {
    const index = this.selectedAuthors.findIndex(item => item.authorId === author.authorId);

    if (index !== -1) {
      this.selectedAuthors.splice(index, 1);
    }
  }
}
