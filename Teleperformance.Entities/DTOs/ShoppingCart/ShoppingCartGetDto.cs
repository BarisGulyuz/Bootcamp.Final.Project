using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Entities.DTOs.ShoppingCart
{
    public class ShoppingCartGetDto : EntityGetDto
    {
        public User User { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public string Title { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsShoppingStarted { get; set; }
    }
}
