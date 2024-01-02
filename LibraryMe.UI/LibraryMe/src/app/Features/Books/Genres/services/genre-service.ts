import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GenreDTO} from "../models/genre-dto";

@Injectable({
  providedIn: 'root',
})
export class GenreService {
  private baseUrl = 'https://localhost:7176/api/genres'; // Replace with your API base URL

  constructor(private http: HttpClient) {}

  getGenres(): Observable<GenreDTO[]> {
    return this.http.get<GenreDTO[]>(this.baseUrl);
  }
}
