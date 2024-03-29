import { EventEmitter, Inject, Injectable, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userKey = 'fh-user';
  tokenKey = 'fh-token';
  expiryKey = 'fh-expires-at';
  private readonly apiBaseUrl;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiBaseUrl = `${baseUrl}api/auth`;
  }

  register(user: any): Promise<any> {
    return asPromise(this.http.post<any>(`${this.apiBaseUrl}/register`, user));
  }

  login(user: any): Promise<any> {
    return asPromise(this.http.post<any>(`${this.apiBaseUrl}/login`, user));
  }

  requestReset(payload: any): Promise<any> {
    return asPromise(this.http.post<any>(`${this.apiBaseUrl}/request-password-reset`, payload));
  }

  resetPassword(payload: any): Promise<any> {
    return asPromise(this.http.post<any>(`${this.apiBaseUrl}/reset-password`, payload));
  }

  logout() {
    localStorage.removeItem(this.userKey);
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.expiryKey);
  }

  isLoggedIn() {
    const now = new Date();
    const expiresAt = new Date(localStorage.getItem(this.expiryKey)!);
    return !!localStorage.getItem(this.tokenKey) && now < expiresAt;
  }

  persistAuth(user: any, token: string, expires: string): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.expiryKey, expires);
    this.updateUser(user);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUser(): any {
    return JSON.parse(localStorage.getItem(this.userKey) || '{}');
  }

  updateUser(user: any): void {
    localStorage.setItem(this.userKey, JSON.stringify(user));
  }
}
