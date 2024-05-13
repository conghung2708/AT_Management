using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace AT_Management.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public decimal Salary {  get; set; }

        [ForeignKey("Position")]
        public Guid PosId { get; set; }
        public Position Position { get; set; }

    }
}
