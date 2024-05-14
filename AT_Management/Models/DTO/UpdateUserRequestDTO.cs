using System.ComponentModel.DataAnnotations;

namespace AT_Management.Models.DTO
{
    public class UpdateUserRequestDTO
    {
        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "PosId is required.")]
        public Guid PosId { get; set; }
    }
}
