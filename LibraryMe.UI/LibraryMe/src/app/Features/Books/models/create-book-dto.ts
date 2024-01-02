export interface CreateBookDTO {
    title: string;
    description: string;
    imageId: string;
    authorIds: string[];
    genreIds: string[];
}
