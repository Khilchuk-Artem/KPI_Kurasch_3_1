import {BookShortcutDTO} from "../../Books/models/book-shortcut-dto";

export interface BorrowingDTO {
    dateCreated: string;
    dueDate: string;
    borrower: string;
    borrowerVisitorCardId: number;
    status: string;
    books: BookShortcutDTO[];
}
