import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {UserDto} from "../../Auth/models/user-dto";
import {AuthService} from "../../Auth/services/auth-service";
import {NgIf} from "@angular/common";
import {UserSummaryService} from "../services/user-summary-service";
import {UserProfileSummaryDTO} from "../models/user-profile-summary-dto";

@Component({
  selector: 'app-view-profile',
  standalone: true,
  imports: [
    RouterLink,
    NgIf
  ],
  templateUrl: './view-profile.component.html',
  styleUrl: './view-profile.component.css'
})
export class ViewProfileComponent implements OnInit {
  user: UserProfileSummaryDTO | undefined;
  userId=this.route.snapshot.params['id']
  constructor(private authService: AuthService,
              private router:Router,
              private userSummaryService:UserSummaryService,
              private route:ActivatedRoute) { }

  ngOnInit(): void {
    // Subscribe to the user observable from AuthService
    this.userSummaryService.getUserSummaryById(this.userId).subscribe(user => {
      this.user = user;
    });
  }
}
