import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService } from '../../services';
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

  constructor(title: Title, private authService: AuthService, private router: Router) {
  }
}
