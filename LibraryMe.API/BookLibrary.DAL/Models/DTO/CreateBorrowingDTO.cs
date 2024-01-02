namespace BookLibrary.DAL.Models.DTO
{
    public class CreateBorrowingDTO
    {
        public int BorrowerId { get; set; }
        public IEnumerable<Guid> BookIds { get; set; }
    }
}
