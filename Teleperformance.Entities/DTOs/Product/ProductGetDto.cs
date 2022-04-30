using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs;
using Teleperformance.Entities.DTOs.Category;

namespace Teleperformance.Entities.DTOs.Product
{
    public class ProductGetDto : EntityGetDto
    {
        public int CategoryId { get; set; }
        public CategoryGetDto Category { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
}
