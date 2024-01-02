export interface AuthorSummaryDTO {
  id:string,
  name: string;
  surname: string;
  patronymic: string;
  biography: string;
  dateOfBirth: string;
  dateOfDeath?: string | null;
  imageUrl: string;
}
