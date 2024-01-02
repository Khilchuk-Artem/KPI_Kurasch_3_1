import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {BorrowingDTO} from "../models/borrowing-dto";
import {Observable} from "rxjs";
import {BorrowingSummaryDTO} from "../models/borrowing-summary-dto";
import {CreateBorrowingDTO} from "../models/create-borrowing-dto";
import {environment} from "../../../../environments/environment";

@Injectable({
    providedIn: 'root',
})
export class BorrowingsService {
    private apiUrl = 'https://localhost:7176/api/borrowings';

    constructor(private http: HttpClient) {}

    getBorrowingById(id: string): Observable<BorrowingDTO> {
        return this.http.get<BorrowingDTO>(`${environment.apiBaseUrl}/api/borrowings/${id}`);
    }

    getBorrowingSummaries(
        pageSize: number = 10,
        pageNumber: number = 1,
        borrowerCardId: string | null = null,
        borrowerName: string | null = null,
        hideReturned: boolean = false,
        startDate: Date | null = null,
        endDate: Date | null = null
    ): Observable<BorrowingSummaryDTO[]> {
        let url = `${environment.apiBaseUrl}/api/borrowings/summaries?pageSize=${pageSize}&pageNumber=${pageNumber}`;

        if (borrowerCardId !== null) {
            url += `&borrowerCardId=${borrowerCardId}`;
        }

        if (hideReturned) {
            url += `&hideReturned=true`;
        }

        if (borrowerName !== null) {
            url += `&borrowerName=${encodeURIComponent(borrowerName)}`;
        }

        if (startDate !== null) {
            url += `&startDate=${startDate}`;
        }

        if (endDate !== null) {
            url += `&endDate=${endDate}`;
        }

        return this.http.get<BorrowingSummaryDTO[]>(url);
    }

    createBorrowing(dto: CreateBorrowingDTO): Observable<any> {
        return this.http.post(`${environment.apiBaseUrl}/api/borrowings`, dto);
    }

    deleteBorrowing(id: string): Observable<BorrowingDTO> {
        return this.http.delete<BorrowingDTO>(`${environment.apiBaseUrl}/api/borrowings/${id}`);
    }
    returnBorrowing(id: string): Observable<any> {
        const url = `${environment.apiBaseUrl}/api/borrowings/${id}/return`;
        return this.http.put(url, null);
    }
}
