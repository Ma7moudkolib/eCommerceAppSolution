import { Injectable, signal, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { User } from '../models/user';
import { RegisterData } from '../models/register-data';
import { APIService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiService = inject(APIService);
  private router = inject(Router);

  currentUser = signal<User | null>(null);
  isAuthenticated = signal<boolean>(false);

  constructor() {
    this.checkAuth();
  }

  login(credentials: any): Observable<any> {
    return this.apiService.login(credentials).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        // Assuming response.user exists or we need to decode token/fetch user.
        // For now, let's assume we store minimal info or fetch it.
        // If ApiService.login returns {token, user}, we use it.
        // If not, we might need to decode token or fetch user details.
        // The API returns {token: string}, so we might need another call or just store username if sent.
        if (response.user) {
          this.handleAuthSuccess(response.token, response.user);
        } else {
          // Fallback if API only sends token: set generic user or decode
          this.handleAuthSuccess(response.token, { username: credentials.username } as any);
        }
      })
    );
  }

  register(registerData: RegisterData) {
    return this.apiService.register(registerData);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.isAuthenticated.set(false);
    this.currentUser.set(null);
    this.router.navigate(['/auth/login']);
  }

  private handleAuthSuccess(token: string, user: User) {
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
    this.isAuthenticated.set(true);
  }

  checkAuth() {
    const token = localStorage.getItem('token');
    const userJson = localStorage.getItem('user');
    if (token && userJson) {
      this.isAuthenticated.set(true);
      this.currentUser.set(JSON.parse(userJson));
    }
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
