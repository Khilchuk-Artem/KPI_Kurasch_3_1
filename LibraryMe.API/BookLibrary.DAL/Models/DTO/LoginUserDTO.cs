using System.ComponentModel.DataAnnotations;

namespace BookLibrary.DAL.Models.DTO
{
    public class LoginUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
