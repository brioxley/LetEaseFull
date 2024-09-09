import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit() {
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    this.authService.register(this.email, this.password).subscribe(
      response => {
        console.log('Registration successful', response);
        // Redirect to login page or show success message
        this.router.navigate(['/login']);
      },
      error => {
        console.error('Registration failed', error);
        this.errorMessage = error.error.message || 'Registration failed. Please try again.';
      }
    );
  }
}
