namespace BookLibrary.DAL.Models.DTO
{
    public class CreateVisitorCardDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        //public Guid VisitorMembershipId { get; set; }
        public Guid? VisitorAccountId { get; set; }
    }
}
