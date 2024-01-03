import { Component, Inject } from '@angular/core';
import { Title } from "@angular/platform-browser";
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../services';
import {Router} from "@angular/router";

interface RegisterPayload {
  firstName?: string;
  lastName?: string;
  emailAddress?: string;
  password?: string;
  confirmPassword?: string;
}

@Component({
  selector: 'fh-auth-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{8,}$/;

  isBusy = false;
  hasError = false;
  errorMessage: string | undefined;
  payload: RegisterPayload = {};

  constructor(title: Title, private authService: AuthService, private router: Router) {
    title.setTitle('Register | Facility Hub');
  }

  async register(): Promise<void> {
    this.isBusy = true;

    try {
      const hasError = this.validatePayload();

      if (!hasError) {
        const { token, user, expiresAt } = await this.authService.register(this.payload);
        this.authService.persistAuth(user, token, expiresAt);

        await this.router.navigate(['/']);
      }
    } catch (e: any) {
      this.errorMessage = e.message;
      this.hasError = true;
    } finally {
      this.isBusy = false;
    }
  }

  dismissError(): void {
    this.hasError = false;
    this.errorMessage = undefined;
  }

  private validatePayload(): boolean {
    let message = null;
    if (!this.payload.emailAddress) {
      message = 'An email address is required';
    } else if (!this.emailRegex.test(this.payload.emailAddress)) {
      message = 'Email address is invalid';
    } else if (!this.payload.password) {
      message = 'A password is required';
    } else if (!this.payload.confirmPassword) {
      message = 'A password confirmation is required';
    } else if (!this.passwordRegex.test(this.payload.confirmPassword)) {
      message = 'A password must be at least 8 chars long with a capital letter, number and special char';
    } else if (this.payload.password !== this.payload.confirmPassword) {
      message = 'Password and confirmation do not match';
    }

    if (message) {
      this.errorMessage = message;
      this.hasError = true
    } else {
      this.dismissError();
    }

    return this.hasError;
  }
}
