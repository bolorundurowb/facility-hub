import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService, NotificationService } from '../../services';
import { ActivatedRoute, Router } from '@angular/router';

interface ResetPasswordPayload {
  userId?: string;
  resetCode?: string;
  password?: string;
  confirmPassword?: string;
}

@Component({
  selector: 'fh-auth-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss'
})
export class ResetPasswordComponent implements OnInit {
  passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{8,}$/;

  isBusy = false;
  hasError = false;
  errorMessage: string | undefined;
  payload: ResetPasswordPayload = {};

  constructor(title: Title, private authService: AuthService, private router: Router,
              private notificationService: NotificationService, private route: ActivatedRoute) {
    title.setTitle('Forgot Password | Facility Hub');
  }

  ngOnInit() {
    console.log(this.route.snapshot.queryParams);
    this.payload.userId = this.route.snapshot.queryParams['user-ref'];
    this.payload.resetCode = this.route.snapshot.queryParams['reset-code'];
  }

  async resetPassword() {
    this.isBusy = true;

    try {
      const hasError = this.validatePayload();

      if (!hasError) {
        await this.authService.resetPassword(this.payload);

        this.notificationService.showSuccess('Your password has been reset successfully! You can proceed to log in.');
        this.notificationService.show('You will be auto-redirected in 3 seconds');

        this.payload = {};
        setTimeout(async () => {
          await this.router.navigate([ 'auth', 'login' ]);
        }, 3000);
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
    if (!this.payload.password) {
      message = 'A password is required';
    } else if (!this.payload.confirmPassword) {
      message = 'A password confirmation is required';
    } else if (!this.passwordRegex.test(this.payload.password)) {
      message = 'A password must be at least 8 chars long with a capital letter, number and special char';
    } else if (this.payload.password !== this.payload.confirmPassword) {
      message = 'Password and confirmation do not match';
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
