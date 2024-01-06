import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService } from '../../services';
import { Router } from '@angular/router';

interface LoginPayload {
  emailAddress?: string;
  password?: string;
}

@Component({
  selector: 'fh-auth-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  isBusy = false;
  hasError = false;
  errorMessage: string | undefined;
  payload: LoginPayload = {};

  constructor(title: Title, private authService: AuthService, private router: Router) {
    title.setTitle('Sign In | Facility Hub');
  }

  async login(): Promise<void> {
    this.isBusy = true;

    try {
      const hasError = this.validatePayload();

      if (!hasError) {
        const { token, user, expiresAt } = await this.authService.login(this.payload);
        this.authService.persistAuth(user, token, expiresAt);

        await this.router.navigate([ 'dashboard' ]);
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
    }

    if (message) {
      this.errorMessage = message;
      this.hasError = true;
    } else {
      this.dismissError();
    }

    return this.hasError;
  }
}
