import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService, NotificationService } from '../../services';
import { Router } from '@angular/router';

interface ForgotPasswordPayload {
  emailAddress?: string;
}

@Component({
  selector: 'fh-auth-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss'
})
export class ForgotPasswordComponent {
  emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  isBusy = false;
  isSuccess = false;
  hasError = false;
  errorMessage: string | undefined;
  payload: ForgotPasswordPayload = {};

  constructor(title: Title, private authService: AuthService, private router: Router,
              private notificationService: NotificationService) {
    title.setTitle('Forgot Password | Facility Hub');
  }

  async requestReset() {
    this.isBusy = true;

    try {
      const hasError = this.validatePayload();

      if (!hasError) {
        await this.authService.requestReset(this.payload);
        this.isSuccess = true;
      }
    } catch (e: any) {
      this.errorMessage = e;
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
