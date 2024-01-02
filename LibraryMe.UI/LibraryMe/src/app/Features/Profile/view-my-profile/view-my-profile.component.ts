import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {UserDto} from "../../Auth/models/user-dto";
import {AuthService} from "../../Auth/services/auth-service";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-view-my-profile',
  standalone: true,
  imports: [
    RouterLink,
    NgIf
  ],
  templateUrl: './view-my-profile.component.html',
  styleUrl: './view-my-profile.component.css'
})
export class ViewMyProfileComponent implements OnInit {
  user: UserDto | undefined;

  constructor(private authService: AuthService,
              private router:Router) { }

  ngOnInit(): void {
    // Subscribe to the user observable from AuthService
    this.authService.user().subscribe(user => {
      this.user = user;
    });
    this.authService.$user.next(this.authService.getUser())
    //// Fetch user information from local storage
    //this.user = this.authService.getUser();
  }

  onLogout(){
    this.router.navigate(['/'])
    this.authService.logout()
  }
  isLibrarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
  canAccessProfile(){
    return this.authService.getUser()?.userId ===this.user?.userId

  }
}
