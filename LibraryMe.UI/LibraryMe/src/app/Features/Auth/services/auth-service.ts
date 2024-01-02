import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {RegisterUserDTO} from "../models/register-user-dto";
import {BehaviorSubject, Observable} from "rxjs";
import {LoginUserDTO} from "../models/login-user-dto";
import {LoginResponseDTO} from "../models/login-response-dto";
import {UserDto} from "../models/user-dto";
import {UserProfileSummaryDTO} from "../../Profile/models/user-profile-summary-dto";
import {CookieService} from "ngx-cookie-service";
import {environment} from "../../../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    $user = new BehaviorSubject<UserDto | undefined>(undefined);

    constructor(private http: HttpClient,
                private cookieService:CookieService) { }

    register(user: RegisterUserDTO): Observable<any> {
        return this.http.post(`${environment.apiBaseUrl}/api/auth/register`, user);
    }

    login(user: LoginUserDTO): Observable<LoginResponseDTO> {
        return this.http.post<LoginResponseDTO>(`${environment.apiBaseUrl}/api/auth/login`, user);
    }
    setUser(response:LoginResponseDTO){
        this.$user.next(response)
        localStorage.setItem('Name', response.name)
        localStorage.setItem('Roles', response.roles.join(','))
        localStorage.setItem('Email', response.email)
        localStorage.setItem('UserId', response.userId)
        localStorage.setItem('CardId', response.visitorsCardId)
    }
    user() : Observable<UserDto | undefined> {
        return this.$user.asObservable();
    }

    getUser():UserDto|undefined{
        const email = localStorage.getItem('Email');
        const roles = localStorage.getItem('Roles');
        const name = localStorage.getItem('Name');
        const id = localStorage.getItem('UserId');
        const cardId=localStorage.getItem('CardId')
      if(cardId){
        if (email && roles && name && id && cardId) {
          return {
            email: email,
            userId:id,
            name: name,
            roles: roles.split(','),
            visitorsCardId:cardId
          };
        }
      }
      else {
        if (email && roles && name && id) {
          return {
            email: email,
            userId:id,
            name: name,
            roles: roles.split(','),
            visitorsCardId:""
          };
        }
      }


        return undefined;
    }
    logout() {
        this.$user.next(undefined);
        localStorage.clear();
        this.cookieService.delete('Authorization', '/');
    }
    updateUser(summary:UserProfileSummaryDTO){
      localStorage.setItem('Name', summary.name)
      localStorage.setItem('CardId', summary.visitorCardId)
      this.$user.next(this.getUser())
    }
}
