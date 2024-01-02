using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
