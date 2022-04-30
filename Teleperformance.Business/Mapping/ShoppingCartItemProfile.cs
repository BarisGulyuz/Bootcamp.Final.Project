using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Business.Mapping
{
    public class ShoppingCartItemProfile : Profile
    {
        public ShoppingCartItemProfile()
        {
            CreateMap<ShoppingCartItemAddDto, ShoppingCartItem>();
            CreateMap<ShoppingCartItemUpdateDto, ShoppingCartItem>();
            CreateMap<ShoppingCartItemGetDto, ShoppingCartItem>().ReverseMap();
        }
    }
}
