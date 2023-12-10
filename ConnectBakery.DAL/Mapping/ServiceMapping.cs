
using AutoMapper;
using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Domain.Entities;

namespace ConnectBakery.DAL.Mapping
{
    public class ServiceMapping : Profile
    {
        public ServiceMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Employe, EmployeDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Stock, StockDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            // Ajoutez d'autres mappings si nécessaire
        }
    }
}
