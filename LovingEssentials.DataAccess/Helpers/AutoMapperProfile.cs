using AutoMapper;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;

namespace LovingEssentials.DataAccess.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Order, OrderDTO>()
                .ReverseMap();

            CreateMap<Cart ,CartDTO>();
            CreateMap<Product, CartDTO>();

            CreateMap<Address, CreateAddressDTO>();
            CreateMap<CreateAddressDTO, Address>();
            CreateMap<UserAddress, User>();
            CreateMap<Address,UserInfo>()
                .ForMember(dest =>dest.PhoneNumber, opt => opt.MapFrom(a => a.Users.PhoneNumber))
                .ForMember(dest =>dest.Email, opt => opt.MapFrom(a => a.Users.Email))
                .ForMember(dest =>dest.Name, opt => opt.MapFrom(a => a.Users.Name));
            CreateMap<Address, UserAddressDTO>();

        }
    }
}
