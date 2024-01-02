import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../services/auth-service";
import {LoginUserDTO} from "../models/login-user-dto";
import {CookieService} from "ngx-cookie-service";
import {Router, RouterLink} from "@angular/router";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(private authService: AuthService,
              private formBuilder: FormBuilder,
              private cookieService:CookieService,
              private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const user: LoginUserDTO = {
        email: this.loginForm.value.email,
        password: this.loginForm.value.password,
      };

      this.authService.login(user).subscribe(
          (response) => {
            console.log('User logged in successfully', response)
            this.cookieService.set('Authorization', `Bearer ${response.token}`,
                undefined,'/',undefined,true,"Strict");
            this.authService.setUser(response)

            this.router.navigate(['']);
          },
          (error) => console.error('Error logging in user', error)
      );
    }
  }
}
