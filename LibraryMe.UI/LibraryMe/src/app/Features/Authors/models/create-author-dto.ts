export interface CreateAuthorDTO {
  name: string;
  surname: string;
  patronymic: string;
  biography: string;
  dateOfBirth: string;
  dateOfDeath?: string | null;
  imageId: string;
}
