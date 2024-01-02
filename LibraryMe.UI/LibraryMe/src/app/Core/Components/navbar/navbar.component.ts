import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {AuthService} from "../../../Features/Auth/services/auth-service";
import {UserDto} from "../../../Features/Auth/models/user-dto";
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterLink,
    NgIf,
    FormsModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent  implements OnInit {
  user?: UserDto;

  constructor(private authService: AuthService,
              private router: Router) {
  }
  myQuery:string="";
  ngOnInit(): void {
    this.authService.user()
        .subscribe({
          next: (response) => {
            this.user = response;
          }
        });

    this.user = this.authService.getUser();

  }
  checkClick(){
    console.log(this.myQuery)
  }
  isLibrarian(){
    return this.authService.getUser()?.roles.includes('Librarian')
  }
}
