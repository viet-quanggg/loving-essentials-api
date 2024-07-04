using AutoMapper;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Admin;
using LovingEssentials.DataAccess.DTOs.Shipper;
using System;

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
                .ForMember(dest => dest.ShipperName, opt => opt.MapFrom(src => src.Shippers.Name))
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products.Name))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Products.ImageURL))
                .ReverseMap();

            CreateMap<Cart ,CartDTO>();
            CreateMap<Product, CartDTO>();

            CreateMap<Address, CreateAddressDTO>();
            CreateMap<CreateAddressDTO, Address>();
            
            CreateMap<UserAddress, User>();

            CreateMap<User, UserInfo>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(a => a.Name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(a => a.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(a => a.Email));
            CreateMap<Address, UserAddressDTO>()
                .ForMember(dest => dest.UserInformation, opt => opt.MapFrom(a => a.Users));

            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, EditProductDTO>();
            CreateMap<OrderResponse, Order>();
            CreateMap<OrderDetailResponse, OrderDetail>();
            CreateMap<UserProfileDTO, User>();

            CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.Buyers, act => act.MapFrom(src => src.Buyers))
            .ForMember(dest => dest.OrderDetails, act => act.MapFrom(src => src.OrderDetails));

            CreateMap<User, UserProfileDTO>();
            CreateMap<OrderDetail, OrderDetailResponse>()
                .ForMember(dest => dest.Products, act => act.MapFrom(src => src.Products));
            CreateMap<Product, ProductDTO>();


        }
    }
}
