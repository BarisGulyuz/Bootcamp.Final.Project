using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs;

namespace Teleperformance.Entities.DTOs.ShoppingCartItems
{
    public class ShoppingCartItemAddDto : EntityCreateDto
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public string Note { get; set; }
    }
}
