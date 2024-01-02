namespace BookLibrary.Models.Domain
{
    public class VisitorsCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        //public Guid VisitorMembershipId { get; set; }
        //public Guid? VisitorAccountId { get; set; }
        public bool IsDeleted { get; set; }

        //public VisitorMembership VisitorMembership { get; set; }
    }
}
