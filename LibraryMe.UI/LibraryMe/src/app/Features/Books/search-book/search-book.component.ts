import {Component, OnInit} from '@angular/core';
import {BookService} from "../services/book-service";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {BookDTO} from "../models/book-dto";
import {query} from "@angular/animations";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-search-book',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    RouterLink
  ],
  templateUrl: './search-book.component.html',
  styleUrl: './search-book.component.css'
})
export class SearchBookComponent implements OnInit{
ngOnInit() {
  this.route.queryParams.subscribe((params)=>{
    this.query =params['searchQuery'];
    this.bookService.getBookSummaries(this.pageSize,this.pageNumber,"",this.query).subscribe((response)=>{
      this.results=response;

      console.log(response)
      this.holdonMsg="No results found"
    })
  })
  this.results=[]
  console.log(this.query)
}
  query = this.route.snapshot.queryParams['searchQuery'];
  results!:BookDTO[]
  pageSize=10;
  pageNumber=1;
  holdonMsg="Loading"
  constructor(private bookService:BookService,
              private route:ActivatedRoute) {
  }
  loadMore(){
    this.pageNumber++;

    this.bookService.getBookSummaries(this.pageSize,this.pageNumber,"",this.query).subscribe((response)=>{
      this.results=[...this.results,...response];
    })
  }
}
