using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.ShoppingCart;

namespace Teleperformance.Business.Mapping
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCartAddDto, ShoppingCart>();
            CreateMap<ShoppingCartUpdateDto, ShoppingCart>();
            CreateMap<ShoppingCartGetDto, ShoppingCart>().ReverseMap();

        }
    }
}
