using AutoMapper;
using FirstWeb.API.Model.Domain;
using FirstWeb.API.Model.DTO;

namespace FirstWeb.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<AddProductRequestDto, Product>().ReverseMap();
            CreateMap<UpdateProductRequestDto, Product>().ReverseMap();
        }
    }
}
