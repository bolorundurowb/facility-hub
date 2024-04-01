import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';
import { Observable } from 'rxjs';

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

  uploadDocument(facilityId: string, payload: any): Observable<any> {
    const formData = new FormData();
    formData.append('type', payload.type);
    formData.append('file', payload.file);

    return this.http.post<any>(`${this.apiBaseUrl}/${facilityId}/documents`, formData, {
      reportProgress: true,
      observe: 'events',
      responseType: 'json'
    });
  }

  deleteDocument(facilityId: string, documentId: string): Promise<any> {
    return asPromise(this.http.delete<any>(`${this.apiBaseUrl}/${facilityId}/documents/${documentId}`));
  }

  getOneIssues(facilityId: string): Promise<any[]> {
    return asPromise(this.http.get<any>(`${this.apiBaseUrl}/${facilityId}/issues`));
  }

  create(payload: any): Promise<any> {
    return asPromise(this.http.post<any>(this.apiBaseUrl, payload));
  }
}
