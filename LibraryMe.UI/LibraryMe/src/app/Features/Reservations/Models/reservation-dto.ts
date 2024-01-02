import {BookShortcutDTO} from "../../Books/models/book-shortcut-dto";

export interface ReservationDTO {
    reservationId: number;
    createdTime: string; // Adjust the type based on your DateTime format
    acceptedTime?: string | null; // Adjust the type based on your DateTime format
    checkOutTime?: string | null; // Adjust the type based on your DateTime format
    status: string;
    books: BookShortcutDTO[];
}
