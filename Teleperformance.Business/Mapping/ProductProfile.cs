using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.Product;

namespace Teleperformance.Business.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAddDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<ProductGetDto, Product>().ReverseMap();
        }
    }
}
