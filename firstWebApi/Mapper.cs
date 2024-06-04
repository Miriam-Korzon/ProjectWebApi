using AutoMapper;
using DTO;
using Entities.Models;


namespace firstWebApi
{
    public class Mapper : Profile
    {
        public Mapper()
        { 
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ForMember(dest => dest.CategoryName,
                opts => opts.MapFrom(src => src.Category.CategoryName)).ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        }
    }
}
