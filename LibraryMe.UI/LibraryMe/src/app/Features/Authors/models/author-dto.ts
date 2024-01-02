import {BookShortcutDTO} from "../../Books/models/book-shortcut-dto";

export interface AuthorDTO {
  name: string;
  surname: string;
  patronymic: string;
  biography: string;
  dateOfBirth: string; // Assuming DateOnly is represented as a string, adjust as needed
  dateOfDeath?: string | null;
  imageUrl: string;
  books: BookShortcutDTO[];
}
