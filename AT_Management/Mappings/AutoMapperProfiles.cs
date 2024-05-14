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
            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Position, AddPositionRequestDTO>().ReverseMap();
            CreateMap<Position, UpdatePositionRequestDTO>().ReverseMap();
            CreateMap<FormType, FormTypeDTO>().ReverseMap();
            CreateMap<FormType, AddFormTypeRequestDTO>().ReverseMap();
            CreateMap<FormType, UpdateFormTypeRequestDTO>().ReverseMap();
        }
    }
}
