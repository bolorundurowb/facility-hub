import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';

@Injectable({
  providedIn: 'root'
})
export class InvitationsService {
  private readonly apiBaseUrl;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiBaseUrl = `${baseUrl}api/invitations`;
  }

  inviteTenant(facilityId: string, payload: any): Promise<any> {
    payload.facilityId = facilityId;
    return asPromise(this.http.post<any>(`${this.apiBaseUrl}/tenant`, payload));
  }
}
