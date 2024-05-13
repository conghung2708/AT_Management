using System.ComponentModel.DataAnnotations;

namespace AT_Management.Models.Domain
{
    public class Form
    {
        [Key]
        public Guid FormId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
