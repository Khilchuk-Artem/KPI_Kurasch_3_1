using System.ComponentModel.DataAnnotations;

namespace BookLibrary.DAL.Models.Domain
{
    public class Author
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Biography { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly? DateOfDeath { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ImageId {  get; set; }

        public IEnumerable<Book> Books { get; set; }
        
        public Image Image { get; set; }
    }
}
