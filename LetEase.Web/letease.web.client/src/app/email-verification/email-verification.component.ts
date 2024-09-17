import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-email-verification',
  templateUrl: './email-verification.component.html',
  styleUrls: ['./email-verification.component.scss']
})
export class EmailVerificationComponent implements OnInit {
  verificationStatus: 'verifying' | 'success' | 'error' = 'verifying';
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const token = params['token'];
      const email = params['email'];
      if (token && email) {
        this.verifyEmail(token, email);
      } else {
        this.verificationStatus = 'error';
        this.errorMessage = 'Invalid verification link.';
      }
    });
  }

  private verifyEmail(token: string, email: string) {
    this.authService.verifyEmail(token, email).subscribe(
      () => {
        this.verificationStatus = 'success';
        setTimeout(() => this.router.navigate(['/login']), 3000);
      },
      error => {
        this.verificationStatus = 'error';
        this.errorMessage = error.error.message || 'Email verification failed. Please try again.';
      }
    );
  }
}
