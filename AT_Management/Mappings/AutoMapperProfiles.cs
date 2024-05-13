using AT_Management.Models.Domain;
using AT_Management.Models.DTO;
using AutoMapper;

namespace AT_Management.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserRequestDTO>().ReverseMap();

        }
    }
}
