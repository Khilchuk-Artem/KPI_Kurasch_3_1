import {Component, Input, OnInit} from '@angular/core';
import {GenreDTO} from "../Genres/models/genre-dto";
import {BookShortcutDTO} from "../models/book-shortcut-dto";
import {BookService} from "../services/book-service";
import {NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {BookDTO} from "../models/book-dto";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-view-books-by-genre',
  standalone: true,
  imports: [
    NgForOf,
    NgOptimizedImage,
    NgIf,
    RouterLink
  ],
  templateUrl: './view-books-by-genre.component.html',
  styleUrl: './view-books-by-genre.component.css'
})
export class ViewBooksByGenreComponent implements OnInit {
  @Input() genre!: GenreDTO;
  pageNumber = 1;
  pageSize = 5;
  books: BookShortcutDTO[] = [];

  constructor(private bookService: BookService) {}
  hasMore=true;
  loading=false;
  waitMsg="Loading..."
  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    // Assuming genre.id is the identifier for the genre
    const genreId = this.genre.id;
    this.loading=true;
    this.bookService.getBookShortcuts(this.pageSize, this.pageNumber, genreId).subscribe(
        (books: BookShortcutDTO[]) => {
          this.books = books;
          this.loading=false;
          this.waitMsg="No books found"
        },
        (error) => {
          console.error('Error fetching books:', error);
          // Handle error as needed
        }
    );
  }
  loadMoreBooks(): void {
    // Increment the page number
    this.pageNumber++;
    this.loading=true;

    const genreId = this.genre.id;

    this.bookService.getBookShortcuts(this.pageSize, this.pageNumber, genreId).subscribe(
        (moreBooks: BookShortcutDTO[]) => {
          this.loading=false;

          // Append the newly loaded books to the existing list
          this.books = [...this.books, ...moreBooks];
          if(moreBooks.length===0){
            this.hasMore=false;
          }
        },
        (error) => {
          console.error('Error fetching more books:', error);
        }
    );
  }
}
