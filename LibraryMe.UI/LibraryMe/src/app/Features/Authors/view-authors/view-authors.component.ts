import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";
import {AuthorSummaryDTO} from "../models/author-summary-dto";
import {AuthorService} from "../services/author.service";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {AuthService} from "../../Auth/services/auth-service";

@Component({
  selector: 'app-view-authors',
  standalone: true,
  imports: [
    RouterLink,
    DatePipe,
    FormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './view-authors.component.html',
  styleUrl: './view-authors.component.css'
})
export class ViewAuthorsComponent {
  authors: AuthorSummaryDTO[] = [];
  searchQuery: string = '';
  pageSize=10;
  pageNumber=1;
  holdOnMsg="Loading..."
  enableLoadMore=true;
  hasNext=true;
  constructor(private authorService: AuthorService,
              private authService:AuthService) {}
  ngOnInit() {
    this.loadAuthors();
  }
  loadMore(){
    this.enableLoadMore=false;
    this.pageNumber++;
    this.authorService.getAuthorSummaries(this.pageSize,this.pageNumber,this.searchQuery).subscribe(
      (authors) => {
        this.authors = [...this.authors,...authors];
        console.log(authors)
        this.enableLoadMore=true;
        if(authors.length===0) this.hasNext=false
      },
      (error) => {
        console.error('Error fetching authors:', error);
      }
    );
  }
  loadAuthors() {
    this.enableLoadMore=false;
    this.authorService.getAuthorSummaries(this.pageSize,this.pageNumber,this.searchQuery).subscribe(
      (authors) => {
        this.authors = authors;
        console.log(authors)
        this.holdOnMsg="No authors found"
        this.enableLoadMore=true;

      },
      (error) => {
        console.error('Error fetching authors:', error);
        this.enableLoadMore=true;

      }
    );
    console.log("yeahhhh")

  }
  updateAuthorList(){
    this.pageNumber=1;
    this.enableLoadMore=false;
    this.hasNext=true;
    this.authorService.getAuthorSummaries(this.pageSize,this.pageNumber,this.searchQuery).subscribe(
      (authors) => {
        this.authors = authors;
        console.log(authors)
      },
      (error) => {
        console.error('Error fetching authors:', error);
      }
    );
    this.enableLoadMore=true;
  }
  isLibarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
}
