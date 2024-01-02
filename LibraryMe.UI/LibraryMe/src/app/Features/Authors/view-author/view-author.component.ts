import { Component } from '@angular/core';
import {AuthorDTO} from "../models/author-dto";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {AuthorService} from "../services/author.service";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {AuthService} from "../../Auth/services/auth-service";

@Component({
  selector: 'app-view-author',
  standalone: true,
    imports: [
        NgIf,
        NgForOf,
        DatePipe,
        RouterLink
    ],
  templateUrl: './view-author.component.html',
  styleUrl: './view-author.component.css'
})
export class ViewAuthorComponent {
  authorId!: string;
  author!: AuthorDTO;

  constructor(
    private route: ActivatedRoute,
    private authorService: AuthorService,
    private authService:AuthService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.authorId = this.route.snapshot.params['id'];//params['id'];
      this.loadAuthorDetails();
    });
  }

  private loadAuthorDetails(): void {
    this.authorService.getAuthorById(this.authorId).subscribe(
      (author: AuthorDTO) => {
        this.author = author;
        console.log(author)
      },
      (error) => {
        console.error('Error fetching author details:', error);
        // Handle error
      }
    );
  }
  isLibarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
}
