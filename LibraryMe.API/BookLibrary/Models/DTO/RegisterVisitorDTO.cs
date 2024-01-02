namespace BookLibrary.Models.DTO
{
    public class RegisterVisitorDTO
    {
        public string Name {  get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public Guid MembershipId { get; set; }
    }
}
