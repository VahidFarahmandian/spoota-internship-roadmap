
using AutoMapper;
using NetProject.Dto;
using NetProject.model;

namespace NetProject.profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<RegisterUser, RegisterUserDto>().ReverseMap();

        }
    }
}
