using AT_Management.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace AT_Management.Models.DTO
{
    public class FormDTO
    {
        
        public FormType FormType { get; set; }
        public string Description { get; set; }
      
    }
}
