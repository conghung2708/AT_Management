using System.ComponentModel.DataAnnotations;

namespace AT_Management.Models.DTO
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address format.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "PosId is required.")]
        public Guid PosId { get; set; }

        // No validation rules specified for Roles property
        public string[] Roles { get; set; }
    }
}
