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
    Username: '',
      Email: '',
      Password: '',
      FirstName: '',
      LastName: '',
      Type: UserType.Client, // Default value
      Role: UserRole.Client, // Default value
      CompanyId: undefined,

  };
  ConfirmPassword: string = '';
  passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
  errorMessage = '';
  successMessage = '';
  constructor(private authService: AuthService) { }

  onSubmit() {
    console.log('User object before sending:', this.user);
    if (this.user.Password !== this.ConfirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    this.authService.register(this.user).subscribe(
      response => {
        this.successMessage = response.message || 'Registration successful. Please check your email to verify your account.';
          this.errorMessage = '';
     },
 error => {
              this.errorMessage = error.error.message || 'Registration failed. Please try again.';
              this.successMessage = '';
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
