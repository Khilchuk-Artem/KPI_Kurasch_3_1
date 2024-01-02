namespace BookLibrary.DAL.Models.DTO
{
    public class AuthorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Biography { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly? DateOfDeath { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<BookShortcutDTO> Books { get; set; }
    }
}
