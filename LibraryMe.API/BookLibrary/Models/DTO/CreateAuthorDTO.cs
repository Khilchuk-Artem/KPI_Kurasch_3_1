namespace BookLibrary.Models.DTO
{
    public class CreateAuthorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Biography { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly? DateOfDeath { get; set; }
        public Guid ImageId { get; set; }
    }
}
