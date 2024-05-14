using System.ComponentModel.DataAnnotations;

namespace AT_Management.Models.DTO
{
    public class UpdateUserRequestDTO
    {

        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public Guid PosId { get; set; }
    }
}
