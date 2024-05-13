using AT_Management.Models.Domain;

namespace AT_Management.Models.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public Position Position { get; set; }
    }
}
