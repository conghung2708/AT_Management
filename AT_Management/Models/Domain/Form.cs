using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AT_Management.Models.Domain
{
    public class Form
    {
        [Key]
        public Guid FormId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey(nameof(FormType))]
        public Guid FormTypeId { get; set; }
        public FormType FormType { get; set; }
        public string Description { get; set; }
        
        public Guid ImageId { get; set; }

        public Image Image { get; set; }
    }
}
