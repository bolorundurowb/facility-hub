import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { asPromise } from '../utils';
import { Observable } from 'rxjs';
import { IssueTransitions } from '../components';

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

  getOneLogs(issueId: string): Promise<any[]> {
    return asPromise(this.http.get<any[]>(`${this.apiBaseUrl}/${issueId}/logs`));
  }

  report(facilityId: string, payload: any): Promise<any> {
    payload.facilityId = facilityId;
    return asPromise(this.http.post<any>(`${this.apiBaseUrl}/report`, payload));
  }

  uploadDocument(issueId: string, payload: any): Observable<any> {
    const formData = new FormData();
    formData.append('type', payload.type);
    formData.append('file', payload.file);

    return this.http.post<any>(`${this.apiBaseUrl}/${issueId}/documents`, formData, {
      reportProgress: true,
      observe: 'events',
      responseType: 'json'
    });
  }

  deleteDocument(issueId: string, documentId: string): Promise<any> {
    return asPromise(this.http.delete<any>(`${this.apiBaseUrl}/${issueId}/documents/${documentId}`));
  }

  transition(issueId: string, nextStatus: IssueTransitions, payload: any): Promise<any> {
    return asPromise(this.http.patch(`${this.apiBaseUrl}/${issueId}/${nextStatus}`, payload));
  }
}
