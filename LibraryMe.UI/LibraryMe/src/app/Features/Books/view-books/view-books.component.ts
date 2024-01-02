import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {GenreService} from "../Genres/services/genre-service";
import {GenreDTO} from "../Genres/models/genre-dto";
import {NgForOf, NgIf} from "@angular/common";
import {ViewBooksByGenreComponent} from "../view-books-by-genre/view-books-by-genre.component";
import {AuthService} from "../../Auth/services/auth-service";

@Component({
  selector: 'app-view-books',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf,
    ViewBooksByGenreComponent,
    NgIf
  ],
  templateUrl: './view-books.component.html',
  styleUrl: './view-books.component.css'
})
export class ViewBooksComponent implements OnInit{
  genres!:GenreDTO[]

  constructor(private genreService:GenreService,
              private authService:AuthService) {

  }
  ngOnInit() {
    this.genreService.getGenres().subscribe((genres)=>{
      this.genres=genres
      console.log(genres)
    }
    )
  }
  isLibrarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
}
