import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {AuthorDTO} from "../models/author-dto";
import {AuthorSummaryDTO} from "../models/author-summary-dto";
import {CreateAuthorDTO} from "../models/create-author-dto";
import {AuthorLinkDTO} from "../models/author-link-dto";
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private baseUrl = 'https://localhost:7176';

  constructor(private http: HttpClient) {}

  getAuthorById(id: string): Observable<AuthorDTO> {
    return this.http.get<AuthorDTO>(`${this.baseUrl}/api/authors/${id}`);
  }

  getAuthors(pageSize: number = 5, pageNumber: number = 1): Observable<AuthorDTO[]> {
    return this.http.get<AuthorDTO[]>(`${environment.apiBaseUrl}/api/authors?pageSize=${pageSize}&pageNumber=${pageNumber}`);
  }

  getAuthorSummaries(pageSize: number = 5, pageNumber: number = 1, searchQuery:string=""): Observable<AuthorSummaryDTO[]> {
    return this.http.get<AuthorSummaryDTO[]>(`${environment.apiBaseUrl}/api/authors/summaries?pageSize=${pageSize}&pageNumber=${pageNumber}&searchQuery=${searchQuery}`);
  }

  createAuthor(dto: CreateAuthorDTO): Observable<any> {
    return this.http.post(`${environment.apiBaseUrl}/api/authors`, dto);
  }

  updateAuthor(id: string, dto: CreateAuthorDTO): Observable<AuthorDTO> {
    return this.http.put<AuthorDTO>(`${environment.apiBaseUrl}/api/authors/${id}`, dto);
  }

  deleteAuthor(id: string): Observable<AuthorDTO> {
    return this.http.delete<AuthorDTO>(`${environment.apiBaseUrl}/api/authors/${id}`);
  }
  getAuthorLinks(): Observable<AuthorLinkDTO[]> {
    return this.http.get<AuthorLinkDTO[]>(`${environment.apiBaseUrl}/api/authors/links`);
  }
}
