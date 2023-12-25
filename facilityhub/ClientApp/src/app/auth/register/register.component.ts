import { Component } from "@angular/core";
import { Title } from "@angular/platform-browser";

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

  constructor(title: Title) {
    title.setTitle('Register | Facility Hub');
  }

  register(): void {
    this.isBusy = true;

    try {
      const hasError = this.validatePayload();

      if (!hasError) {
      }
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
    }

    return this.hasError;
  }
}
