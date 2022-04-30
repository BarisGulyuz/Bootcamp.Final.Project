using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs;

namespace Teleperformance.Entities.DTOs.Category
{
    public class CategoryGetDto : EntityGetDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
