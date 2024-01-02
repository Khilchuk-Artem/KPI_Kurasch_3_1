import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../services/auth-service";
import {RegisterUserDTO} from "../models/register-user-dto";
import {routes} from "../../../app.routes";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  standalone: true,
    imports: [
        ReactiveFormsModule
    ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registrationForm!: FormGroup;
  constructor(private authService: AuthService,
              private formBuilder: FormBuilder,
              private router:Router) { }

  ngOnInit(): void {
    this.registrationForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(5)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  onSubmit(): void {
    if (this.registrationForm.valid) {
      const user: RegisterUserDTO = {
        name: this.registrationForm.value.name,
        email: this.registrationForm.value.email,
        password: this.registrationForm.value.password,
        roles: ['Reader'] // You can customize this based on your requirements
      };
      console.log(user)
      this.authService.register(user).subscribe(
          (response) => {
            this.router.navigate(['/login'])
          },
          error => console.error('Error registering user', error)
      );
    }
  }
}
