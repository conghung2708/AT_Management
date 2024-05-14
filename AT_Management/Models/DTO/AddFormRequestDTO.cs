namespace AT_Management.Models.DTO
{
    public class AddFormRequestDTO
    {
        public Guid FormTypeId { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
