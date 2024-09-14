import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service'; 
import { RegisterUserDto } from '../models/register-user.model';
import { UserType, UserRole } from '../models/user-enums';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent {
  user: RegisterUserDto = {
      email: '',
      password: '',
      firstName: '',
      lastName: '',
      type: UserType.Client, // Default value
      role: UserRole.Client, // Default value
      companyId: undefined,
      username: ''
  };
  confirmPassword: string = '';
  constructor(private authService: AuthService) { }

  onSubmit() {
    if (this.user.password !== this.confirmPassword) {
      console.error('Passwords do not match');
      return;
    }

    this.authService.register(this.user).subscribe(
      response => {
        console.log('Registration successful', response);
        // Handle successful registration
      },
      error => {
        console.error('Registration failed', error);
        // Handle registration error
      }
    );
  }
}

//  constructor(private authService: AuthService, private router: Router) { }

//  onSubmit() {
//    if (this.password !== this.confirmPassword) {
//      this.errorMessage = 'Passwords do not match';
//      return;
//    }

//    const registerUserDto = {
//      email: this.email,
//      password: this.password,
//      firstName: this.firstName,
//      lastName: this.lastName,
//      userName: this.email // Using email as username
//    };

//    this.authService.register(registerUserDto).subscribe(
//      response => {
//        console.log('Registration successful', response);
//        // Redirect to login page or show success message
//        this.router.navigate(['/login']);
//      },
//      error => {
//        console.error('Registration failed', error);
//        this.errorMessage = error.error.message || 'Registration failed. Please try again.';
//      }
//    );
//  }
//}
