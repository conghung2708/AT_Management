using System.ComponentModel.DataAnnotations;

namespace AT_Management.Models.DTO
{
    public class AddPositionRequestDTO
    {

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Basic salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Basic salary must be a non-negative value.")]
        public decimal BasicSalary { get; set; }
    }
}
