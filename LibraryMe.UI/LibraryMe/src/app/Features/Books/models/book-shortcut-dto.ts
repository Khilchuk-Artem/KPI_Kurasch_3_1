import {AuthorLinkDTO} from "../../Authors/models/author-link-dto";

export interface BookShortcutDTO {
  id:string;
  bookId: string;
  title: string;
  imageUrl: string;
  authors: AuthorLinkDTO[];
}
