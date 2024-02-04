import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';

@Injectable({
  providedIn: 'root'
})
export class IssuesService {
  private readonly apiBaseUrl;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiBaseUrl = `${baseUrl}api/issues`;
  }


  getAll(): Promise<any[]> {
    return asPromise(this.http.get<any[]>(this.apiBaseUrl));
  }


  getOne(issueId: string): Promise<any> {
    return asPromise(this.http.get<any>(`${this.apiBaseUrl}/${issueId}`));
  }


  getOneDocuments(issueId: string): Promise<any[]> {
    return asPromise(this.http.get<any[]>(`${this.apiBaseUrl}/${issueId}/documents`));
  }


  report(facilityId: string, payload: any): Promise<any> {
    payload.facilityId = facilityId;
    return asPromise(this.http.post<any>(`${this.apiBaseUrl}/report`, payload));
  }
}
