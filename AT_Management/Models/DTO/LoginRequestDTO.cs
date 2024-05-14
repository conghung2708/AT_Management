using System.ComponentModel.DataAnnotations;

namespace AT_Management.Models.DTO
{
    public class LoginRequestDTO
    {

        [Required(ErrorMessage = "Username is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address format.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
