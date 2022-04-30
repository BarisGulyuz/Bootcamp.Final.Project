using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs;

namespace Teleperformance.Entities.DTOs.Product
{
    public class ProductAddDto : EntityCreateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
