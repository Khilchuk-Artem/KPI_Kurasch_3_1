import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {BookDTO} from "../models/book-dto";
import {BookShortcutDTO} from "../models/book-shortcut-dto";
import {CreateBookDTO} from "../models/create-book-dto";
import {Injectable} from "@angular/core";
import {environment} from "../../../../environments/environment";
@Injectable({
  providedIn: 'root',
})
export class BookService {
    private baseUrl = 'https://localhost:7176';

    constructor(private http: HttpClient) {}

    getBookById(id: string): Observable<BookDTO> {
        return this.http.get<BookDTO>(`${environment.apiBaseUrl}/api/books/${id}`);
    }

    getBookShortcuts(pageSize: number = 5, pageNumber: number = 1, genreId:string=""): Observable<BookShortcutDTO[]> {
        return this.http.get<BookShortcutDTO[]>(`${environment.apiBaseUrl}/api/books/shortcuts?pageSize=${pageSize}&pageNumber=${pageNumber}&genreId=${genreId}`);
    }

    getBookSummaries(pageSize: number = 5, pageNumber: number = 1, genreId:string="", query:string=""): Observable<BookDTO[]> {
        return this.http.get<BookDTO[]>(`${environment.apiBaseUrl}/api/books/summaries?pageSize=${pageSize}&pageNumber=${pageNumber}&genreId=${genreId}&searchQuery=${query}`);
    }

    createBook(dto: CreateBookDTO): Observable<any> {
        return this.http.post(`${environment.apiBaseUrl}/api/books`, dto);
    }

    updateBook(id: string, dto: CreateBookDTO): Observable<BookDTO> {
        return this.http.put<BookDTO>(`${environment.apiBaseUrl}/api/books/${id}`, dto);
    }

    deleteBook(id: string): Observable<BookDTO> {
        return this.http.delete<BookDTO>(`${environment.apiBaseUrl}/api/books/${id}`);
    }
}
