import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AnnouncementDTO } from "../models/announcement-dto";
import {HttpClient} from "@angular/common/http";
import {UpdateAnnouncementDTO} from "../models/update-announcement-dto";
import {environment} from "../../../../environments/environment";

@Injectable({
    providedIn: 'root',
})
export class AnnouncementsService {

    private http: HttpClient;

    constructor(http: HttpClient) {
        this.http = http;
    }

    getAnnouncementById(id: string): Observable<AnnouncementDTO> {
        return this.http.get<AnnouncementDTO>(`${environment.apiBaseUrl}/api/announcements/${id}`);
    }

    getAnnouncements(pageSize: number = 5, pageNumber: number = 1): Observable<AnnouncementDTO[]> {
        return this.http.get<AnnouncementDTO[]>(`${environment.apiBaseUrl}/api/announcements?pageSize=${pageSize}&pageNumber=${pageNumber}`);
    }

    getAnnouncementSummaries(pageSize: number = 5, pageNumber: number = 1): Observable<AnnouncementDTO[]> {
        return this.http.get<AnnouncementDTO[]>(`${environment.apiBaseUrl}/api/announcements/summaries?pageSize=${pageSize}&pageNumber=${pageNumber}`);
    }

    createAnnouncement(dto: AnnouncementDTO): Observable<any> {
        return this.http.post(`${environment.apiBaseUrl}/api/announcements/`, dto);
    }

    updateAnnouncement(id: string, dto: UpdateAnnouncementDTO): Observable<AnnouncementDTO> {
        return this.http.put<AnnouncementDTO>(`${environment.apiBaseUrl}/api/announcements/${id}`, dto);
    }

    deleteAnnouncement(id: string): Observable<AnnouncementDTO> {
        return this.http.delete<AnnouncementDTO>(`${environment.apiBaseUrl}/api/announcements/${id}`);
    }
}
