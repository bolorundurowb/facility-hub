import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class FileDownloadService {
  constructor(private http: HttpClient) {
  }

  downloadFile(url: string, fileName: string): void {
    this.http.get(url, { responseType: 'blob' })
      .subscribe({
        next: (response: Blob) => {
          const blob = new Blob([ response ], { type: 'application/octet-stream' });

          const link = document.createElement('a');
          link.href = URL.createObjectURL(blob);
          link.download = fileName;
          link.click();
        },
        error: (error) => {
          console.error('Error downloading file:', error);
        }
      });
  }
}
