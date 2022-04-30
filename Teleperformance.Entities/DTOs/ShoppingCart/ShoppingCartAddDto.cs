using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs;

namespace Teleperformance.Entities.DTOs.ShoppingCart
{
    public class ShoppingCartAddDto : EntityCreateDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
    }
}
