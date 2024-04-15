using AutoMapper;
using UserManagement.API.Data;
using UserManagement.API.Model.User;

namespace UserManagement.API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, RegisterDTO>().ReverseMap();
        }
    }
}
