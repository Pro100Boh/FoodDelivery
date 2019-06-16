using AutoMapper;
using FoodDeliveryServer.Entities;
using FoodDeliveryServer.Models;
using System.Linq;

namespace FoodDeliveryServer.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pizza, PizzaViewModel>().
               ForMember(dest => dest.Ingradients,
                    opts => opts.MapFrom(src => src.PizzaIngradients.Select(g => g.Ingradient)));

            CreateMap<Ingradient, IngradientViewModel>();

            CreateMap<Drink, DrinkViewModel>();

            CreateMap<Order, OrderViewModel>();

            CreateMap<OrderedProduct, OrderedProductViewModel>();
        }
    }
}
