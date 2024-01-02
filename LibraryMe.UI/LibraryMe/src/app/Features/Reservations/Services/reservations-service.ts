import {HttpClient} from "@angular/common/http";
import {ReservationDTO} from "../Models/reservation-dto";
import {map, Observable} from "rxjs";
import {ReservationSummaryDTO} from "../Models/reservation-summary-dto";
import {CreateReservationDTO} from "../Models/create-reservation-dto";
import {Injectable} from "@angular/core";
import {environment} from "../../../../environments/environment";
@Injectable({
    providedIn: 'root',
})
export class ReservationsService {
    private apiUrl = 'https://localhost:7176/api/reservations';

    constructor(private http: HttpClient) {}

    getReservationById(id: number): Observable<ReservationDTO> {
        return this.http.get<ReservationDTO>(`${environment.apiBaseUrl}/api/reservations/${id}`);
    }

    getReservationSummaries(pageSize: number = 5, pageNumber: number = 1, searchQuery:string="", visitorCardId:string|null=null): Observable<ReservationSummaryDTO[]> {
        var url = `${environment.apiBaseUrl}/api/reservations/summaries?pageSize=${pageSize}&pageNumber=${pageNumber}&searchQuery=${searchQuery}`;
        if(visitorCardId){
            url=url + "&visitorCardId="+visitorCardId.toString()
        }
        return this.http.get<ReservationSummaryDTO[]>(url);
    }

    createReservation(dto: CreateReservationDTO): Observable<any> {
        return this.http.post(`${environment.apiBaseUrl}/api/reservations`, dto);
    }
  declineReservation(id: number): Observable<ReservationDTO> {
    const url = `${environment.apiBaseUrl}/api/reservations/${id}/decline`;
    return this.http.put<ReservationDTO>(url, {}).pipe(
      map(response => response as ReservationDTO)
    );
  }

  acceptReservation(id: number): Observable<ReservationDTO> {
    const url = `${environment.apiBaseUrl}/api/reservations/${id}/accept`;
    return this.http.put<ReservationDTO>(url, {}).pipe(
      map(response => response as ReservationDTO)
    );
  }

  checkOutReservation(id: number): Observable<ReservationDTO> {
    const url = `${environment.apiBaseUrl}/api/reservations/${id}/checkout`;
    return this.http.put<ReservationDTO>(url, {}).pipe(
      map(response => response as ReservationDTO)
    );
  }
}
