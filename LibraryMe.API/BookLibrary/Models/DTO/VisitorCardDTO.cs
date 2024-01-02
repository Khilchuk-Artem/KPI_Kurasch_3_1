using BookLibrary.Models.Domain;

namespace BookLibrary.Models.DTO
{
    public class VisitorCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        //public string VisitorMembershipName { get; set; }
    }
}
