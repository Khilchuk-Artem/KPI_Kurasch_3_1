import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {AuthService} from "../../Auth/services/auth-service";
import {VisitorsCardsService} from "../services/visitor-cards-service";
import {VisitorCardDTO} from "../models/visitor-card-dto";
import {UserDto} from "../../Auth/models/user-dto";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-view-visitor-card',
  standalone: true,
  imports: [
    NgIf,
    RouterLink
  ],
  templateUrl: './view-visitor-card.component.html',
  styleUrl: './view-visitor-card.component.css'
})
export class ViewVisitorCardComponent implements OnInit{
  constructor(private route:ActivatedRoute,
              private authService:AuthService,
              private visitorcardsService:VisitorsCardsService) {
  }
  visitorCard!:VisitorCardDTO;
  user!:UserDto|undefined;
  ngOnInit() {
    this.visitorcardsService.getVisitorCardById(this.route.snapshot.params['id']).subscribe((response)=>{
      this.visitorCard=response;
    })
    this.user=this.authService.getUser()
  }
  isLibrarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
}
