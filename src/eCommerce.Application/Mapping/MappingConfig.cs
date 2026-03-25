using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Entities.Identity;
namespace eCommerce.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateCategory , Category>().ReverseMap();
            CreateMap<GetCategory , Category>().ReverseMap();
            CreateMap<UpdateCategory , Category>().ReverseMap();
            CreateMap<CreateProduct , Product>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();
            CreateMap<GetProduct, Product>().ReverseMap();
            CreateMap<AppUser, CreateUser >().ReverseMap();
            CreateMap<AppUser, LoginUser>().ReverseMap();
            CreateMap<PaymentMethod , GetPaymentMethod>().ReverseMap();
            CreateMap<CreateAchieve , Achieve>().ReverseMap();
        }
    }
}
