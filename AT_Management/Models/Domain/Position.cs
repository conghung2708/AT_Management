using System.ComponentModel.DataAnnotations;

namespace AT_Management.Models.Domain
{
    public class Position
    {

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal BasicSalary { get; set; }
    }
}
