import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';

@Injectable({ providedIn: 'root'})
export class UsersService {
  private readonly apiBaseUrl;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiBaseUrl = `${baseUrl}api/users`;
  }

  getProfile(): Promise<any> {
    return asPromise(this.http.get<any>(`${this.apiBaseUrl}/current`));
  }
}
