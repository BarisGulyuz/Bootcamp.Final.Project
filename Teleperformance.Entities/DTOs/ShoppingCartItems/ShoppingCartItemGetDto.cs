using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs;
using Teleperformance.Entities.DTOs.Product;
using Teleperformance.Entities.DTOs.ShoppingCart;

namespace Teleperformance.Entities.DTOs.ShoppingCartItems
{
    public class ShoppingCartItemGetDto : EntityGetDto
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCartGetDto ShoppingCart { get; set; }
        public ProductGetDto Product { get; set; }
        public string Note { get; set; }
    }
}
