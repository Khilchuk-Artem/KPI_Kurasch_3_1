using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models.DTO
{
    public class LoginResponseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string VisitorsCardId { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
