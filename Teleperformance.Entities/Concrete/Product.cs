using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities;

namespace Teleperformance.Entities.Concrete
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
