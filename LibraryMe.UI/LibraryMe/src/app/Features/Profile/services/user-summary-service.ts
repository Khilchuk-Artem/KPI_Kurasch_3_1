import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {UserProfileSummaryDTO} from "../models/user-profile-summary-dto";
import {UpdateUserDTO} from "../models/update-user-dto";
import {UserSummaryShortcutDTO} from "../models/user-summary-shortcut";
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UserSummaryService {
  private apiUrl = 'https://localhost:7176';

  constructor(private http: HttpClient) { }

  getUserSummaryById(userId: string): Observable<UserProfileSummaryDTO> {
    return this.http.get<UserProfileSummaryDTO>(`${environment.apiBaseUrl}/api/usersummary/${userId}`);
  }
  getUserSummaryShortcuts(pageSize: number = 20,
                          pageNumber: number = 1,
                          searchQuery:string=""): Observable<UserSummaryShortcutDTO[]> {
    return this.http.get<UserSummaryShortcutDTO[]>(`${environment.apiBaseUrl}/api/usersummary/shortcuts?pageSize=${pageSize}&pageNumber=${pageNumber}&query=${searchQuery}`);
  }
  updateUserSummaryById(updatedUserSummary: UpdateUserDTO, userId: string): Observable<UserProfileSummaryDTO> {
    return this.http.put<UserProfileSummaryDTO>(`${environment.apiBaseUrl}/api/usersummary/${userId}`, updatedUserSummary);
  }
}
