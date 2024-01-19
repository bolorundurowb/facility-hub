import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService, NotificationService } from '../../services';
import { Router } from '@angular/router';

interface ForgotPasswordPayload {
  emailAddress?: string;
}

@Component({
  selector: 'fh-auth-forgot-password',
  templateUrl: './forgot.password.component.html',
  styleUrl: './forgot.password.component.scss'
})
export class ForgotPasswordComponent {
  emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  isBusy = false;
  hasError = false;
  errorMessage: string | undefined;
  payload: ForgotPasswordPayload = {};

  constructor(title: Title, private authService: AuthService, private router: Router,
              private notificationService: NotificationService) {
  }

  async requestReset() {
    this.isBusy = true;

    try {
      const hasError = this.validatePayload();

      if (!hasError) {
        await this.authService.requestReset(this.payload);
        this.notificationService.showSuccess("A reset code has been dispatched to your account");
        await this.router.navigate([ 'auth', 'reset-password' ]);
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
