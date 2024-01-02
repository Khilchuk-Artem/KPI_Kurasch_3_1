import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {VisitorCardDTO} from "../models/visitor-card-dto";
import {CreateVisitorCardDTO} from "../models/create-visitor-card-dto";
import {VisitorCardShortcutDTO} from "../models/visitor-card-shortcut-dto";

@Injectable({
  providedIn: 'root'
})
export class VisitorsCardsService {
  private apiUrl = 'https://localhost:7176/api/visitorscards';

  constructor(private http: HttpClient) { }

  getVisitorCardById(id: number): Observable<VisitorCardDTO> {
    return this.http.get<VisitorCardDTO>(`${this.apiUrl}/${id}`);
  }

  getVisitorCards(pageSize: number = 10,
                  pageNumber: number = 1,): Observable<VisitorCardDTO[]> {
    return this.http.get<VisitorCardDTO[]>(`${this.apiUrl}`);
  }
  getVisitorCardShortcuts(
    pageSize: number = 20,
    pageNumber: number = 1,
    searchQuery:string=""
  ): Observable<VisitorCardShortcutDTO[]> {
    return this.http.get<VisitorCardShortcutDTO[]>(`${this.apiUrl}/shortcuts?pageSize=${pageSize}&pageNumber=${pageNumber}&query=${searchQuery}`);
  }
  createVisitorCard(dto: CreateVisitorCardDTO): Observable<any> {
    return this.http.post(this.apiUrl, dto);
  }

  updateVisitorCard(id: number, dto: CreateVisitorCardDTO): Observable<VisitorCardDTO> {
    return this.http.put<VisitorCardDTO>(`${this.apiUrl}/${id}`, dto);
  }

  deleteVisitorCard(id: number): Observable<VisitorCardDTO> {
    return this.http.delete<VisitorCardDTO>(`${this.apiUrl}/${id}`);
  }
}
