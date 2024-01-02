import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {GenreDTO} from "../Genres/models/genre-dto";
import {AuthorLinkDTO} from "../../Authors/models/author-link-dto";
import {ImageService} from "../../../Shared/image.service";
import {AuthorService} from "../../Authors/services/author.service";
import {BookService} from "../services/book-service";
import {ActivatedRoute, Router} from "@angular/router";
import {GenreService} from "../Genres/services/genre-service";
import {HttpErrorResponse} from "@angular/common/http";
import {CreateBookDTO} from "../models/create-book-dto";
import {AsyncPipe, NgForOf} from "@angular/common";
import {NgSelectComponent, NgSelectModule} from "@ng-select/ng-select";
import {Observable} from "rxjs";

@Component({
  selector: 'app-edit-book',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    NgSelectModule,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './edit-book.component.html',
  styleUrl: './edit-book.component.css'
})
export class EditBookComponent implements OnInit {
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
  bookId:string;


  constructor(
      private fb: FormBuilder,
      private imageService: ImageService,
      private authorService: AuthorService,
      private bookService: BookService,
      private router: Router,
      private route: ActivatedRoute,
      private genreService: GenreService
  ) {
    this.bookId = this.route.snapshot.params['id'];
  }

  ngOnInit() {
    this.bookForm = this.fb.group({
      file: [null],
      title: ['', Validators.required],
      description: [''],
    });

    this.loadData();

    this.genres = this.genreService.getGenres();
    this.authors = this.authorService.getAuthorLinks();
  }

  loadData(): void {
    // Load book data using the BookService
    this.bookService.getBookById(this.bookId).subscribe(
        (book) => {
          this.bookForm.patchValue({
            title: book.title,
            description: book.description,
            totalAmount: book.totalAmount,
          });
          this.imageService.getImageIdByUrl(book.imageUrl).subscribe((id)=>{
            this.imageId =id;
          })
          // Assuming imageId is already available in the book data

          this.selectedGenres = book.genres;
          this.selectedAuthors = book.authors;

          // Optionally, load available genres and authors for selection
          this.genres = this.genreService.getGenres();
          this.authors = this.authorService.getAuthorLinks();
        },
        (error) => {
          console.error('Error loading book:', error);

          // Handle error, e.g., navigate to an error page
        }
    );
  }
  onDelete(){
    this.bookService.deleteBook(this.bookId).subscribe((response)=>{
      console.log(response)
      this.router.navigate(['/books'])
    })
  }
  onSubmit(): void {
    if (this.bookForm.valid) {
      const formData = this.bookForm.value as CreateBookDTO;

      formData.imageId = this.imageId.toString();
      formData.genreIds = this.selectedGenres.map(genre => genre.id);
      formData.authorIds = this.selectedAuthors.map(author => author.authorId);
      this.bookService.updateBook(this.bookId, formData).subscribe(
          (updatedBook) => {
            console.log('Book updated successfully:', updatedBook);
            this.router.navigate(['/books', this.bookId]);
          },
          (error) => {
            console.error('Error updating book:', error);

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
