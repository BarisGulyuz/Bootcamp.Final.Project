using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities;

namespace Teleperformance.Entities.Concrete
{
    public class ShoppingCart : BaseEntity
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public DateTime ExpireDate { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public bool IsShoppingStarted { get; set; }
    }
}
