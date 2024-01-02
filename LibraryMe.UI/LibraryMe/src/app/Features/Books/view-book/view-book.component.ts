import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {BookDTO} from "../models/book-dto";
import {BookService} from "../services/book-service";
import {last} from "rxjs";
import {NgForOf, NgIf} from "@angular/common";
import {BookbagService} from "../../Bookbag/services/bookbag-service";
import {AuthService} from "../../Auth/services/auth-service";
import {BookmarkService} from "../../Profile/services/bookmark-service";
import {BookmarkDTO} from "../../Profile/models/bookmark-dto";

@Component({
  selector: 'app-view-book',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf,
    NgIf
  ],
  templateUrl: './view-book.component.html',
  styleUrl: './view-book.component.css'
})
export class ViewBookComponent implements OnInit {
  book: BookDTO | undefined;
  bookMark:BookmarkDTO|undefined;
  userid:string|undefined;
  constructor(private route: ActivatedRoute,
              private bookService: BookService,
              private bookbagService:BookbagService,
              private authService:AuthService,
              private bookMarkService:BookmarkService) { }

  ngOnInit(): void {
    this.userid=this.authService.getUser()?.userId;
    // Get the book ID from the route parameter
    this.route.paramMap.subscribe(params => {
      const bookId = params.get('id');

      if (bookId) {
        // Fetch book details using the BookService
        this.bookService.getBookById(bookId).subscribe(book => {
          this.book = book;
          if(this.userid){
            this.bookMarkService.getBookmarksByUserId(this.userid,bookId).subscribe((response)=>{
              this.bookMark=response[0]
              console.log(this.bookMark)
            })
          }
        });
      }
    });
  }
  onAddToBookbag(){
    if(this.book){
      this.bookbagService.appendItemToBookbag({
        bookId:this.book.id,
        id:"0",
        title:this.book.title,
        authors:this.book.authors,
        imageUrl:this.book.imageUrl
      })
      console.log(this.bookbagService.getBookbag())
    }
  }
  onAddBookmark(){
    if(this.book&&this.userid){
      this.bookMarkService.createBookmark({
        bookId:this.book.id,
        userId:this.userid
      }).subscribe((response)=>{
        this.bookMark=response;
        console.log("Bookmark created",response)
        console.log(this.bookMark)
      })
    }
  }
  onRemoveBookmark(){
    if(this.bookMark){
      this.bookMarkService.deleteBookmark(this.bookMark.id).subscribe((response)=>{
        console.log("Bookmark deleted",response)
        this.bookMark=undefined;
      })
    }
  }
  isLibrarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }

  last = last;
}
