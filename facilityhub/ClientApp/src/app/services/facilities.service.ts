import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';

@Injectable({
  providedIn: 'root'
})
export class FacilitiesService {
  private readonly apiBaseUrl;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiBaseUrl = `${baseUrl}api/facilities`;
  }

  getAll(): Promise<any[]> {
    return asPromise(this.http.get<any[]>(this.apiBaseUrl));
  }

  getOne(facilityId: string): Promise<any> {
    return asPromise(this.http.get<any>(`${this.apiBaseUrl}/${facilityId}`));
  }

  getOneDocuments(facilityId: string): Promise<any[]> {
    return asPromise(this.http.get<any>(`${this.apiBaseUrl}/${facilityId}/documents`));
  }

  getOneIssues(facilityId: string): Promise<any[]> {
    return asPromise(this.http.get<any>(`${this.apiBaseUrl}/${facilityId}/issues`));
  }

  create(payload: any): Promise<any> {
    return asPromise(this.http.post<any>(this.apiBaseUrl, payload));
  }
}
