import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ImageService {
  private baseUrl = 'https://localhost:7176/api/images'; // Replace with your API base URL

  constructor(private http: HttpClient) {}

  uploadImage(file: File): Observable<string> {
    const formData = new FormData();
    formData.append('file', file);

    const headers = new HttpHeaders();

    return this.http.post<string>(`${this.baseUrl}`, formData, {
      headers,
    });
  }
  getImageIdByUrl(link: string): Observable<string> {
    const headers = new HttpHeaders();

    return this.http.get<string>(`${this.baseUrl}/${link.replaceAll("/","%2F")}`,{
      headers,
    });
  }
}
