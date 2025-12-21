import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RegisterData } from '../../models/register-data';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { CheckboxModule } from 'primeng/checkbox';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, RouterLink, InputTextModule, ButtonModule, PasswordModule, CheckboxModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  authService = inject(AuthService);
  private router = inject(Router);

  registerData: RegisterData = {
    email: '',
    password: '',
    username: '',

  };
  confirmPassword = '';
  agreeTerms = false;
  errorMessage = '';
  loading = false;

  onSubmit(): void {
    if (!this.registerData.email || !this.registerData.password ||
      !this.registerData.username) {
      this.errorMessage = 'Please fill in all fields';
      return;
    }

    if (this.registerData.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    if (!this.agreeTerms) {
      this.errorMessage = 'Please agree to the terms and conditions';
      return;
    }

    this.errorMessage = '';
    this.loading = true;
    this.authService.register(this.registerData).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/']);
      },
      error: (error) => {
        this.loading = false;
        this.errorMessage = 'Registration failed. Please try again.';
      }
    });
  }
}
