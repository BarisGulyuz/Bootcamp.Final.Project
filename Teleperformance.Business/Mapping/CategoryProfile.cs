using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.Category;

namespace Teleperformance.Business.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<CategoryGetDto, Category>().ReverseMap();
        }
    }
}
