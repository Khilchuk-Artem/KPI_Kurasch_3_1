import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateBookmarkDTO} from "../models/create-bookmark-dto";
import {Observable} from "rxjs";
import {BookmarkDTO} from "../models/bookmark-dto";
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class BookmarkService {
  private baseUrl = 'https://localhost:7176/api/bookmarks';

  constructor(private httpClient: HttpClient) {}

  getBookmarksByUserId(userId: string, bookId:string=""): Observable<BookmarkDTO[]> {
    const url = `${environment.apiBaseUrl}/api/bookmarks/${userId}?bookId=${bookId}`;
    return this.httpClient.get<BookmarkDTO[]>(url);
  }

  createBookmark(dto: CreateBookmarkDTO): Observable<any> {
    return this.httpClient.post(`${environment.apiBaseUrl}/api/bookmarks`, dto);
  }

  deleteBookmark(id: string): Observable<BookmarkDTO> {
    const url = `${environment.apiBaseUrl}/api/bookmarks/${id}`;
    return this.httpClient.delete<BookmarkDTO>(url);
  }
}
