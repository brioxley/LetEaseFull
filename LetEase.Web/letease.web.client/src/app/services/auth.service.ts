import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs/operators';
import { RegisterUserDto } from '../models/register-user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/login`, { email, password })
           .pipe(
 tap(response => this.setSession(response))
              );
  }

  register(user: any): Observable<any> {
    const registerUserDto = { registerUserDto: user };
    return this.http.post(`${this.apiUrl}/auth/register`, registerUserDto);
  }

//register(registerUserDto: any): Observable < any > {
//    return this.http.post(`${this.apiUrl}/auth/register`, registerUserDto);
//  }

private setSession(authResult: any) {
localStorage.setItem('token', authResult.token);
 localStorage.setItem('user_id', authResult.user.id);
  }

  logout() {
 localStorage.removeItem('token');
 localStorage.removeItem('user_id');
  }

   isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

 getToken(): string | null {
    return localStorage.getItem('token');
 }

  refreshToken(): Observable<any> {
    const currentToken = localStorage.getItem('token');
    return this.http.post<any>(`${this.apiUrl}/auth/refresh-token`, { token: currentToken })
      .pipe(
        tap(response => {
          if (response && response.token) {
            localStorage.setItem('token', response.token);
          }
        })
      );
  }
}
