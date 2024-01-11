import {Injectable} from '@angular/core';
import { AuthService } from '../services';

@Injectable({providedIn: 'root'})
export class AuthGuard {
  constructor(private authService: AuthService) {
  }

  canActivate() {
    if (this.authService.isLoggedIn()) {
      return true;
    }

    window.location.href = '/';
    return false;
  }
}
