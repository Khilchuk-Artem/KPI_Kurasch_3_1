import {GenreDTO} from "../Genres/models/genre-dto";
import {AuthorLinkDTO} from "../../Authors/models/author-link-dto";

export interface BookDTO {
    id:string;
    totalAmount: number;
    availableAmount: number;
    title: string;
    description: string;
    imageUrl: string;
    genres: GenreDTO[];
    authors: AuthorLinkDTO[];
}
