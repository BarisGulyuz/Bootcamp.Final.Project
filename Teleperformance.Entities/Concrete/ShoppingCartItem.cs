using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities;

namespace Teleperformance.Entities.Concrete
{
    public class ShoppingCartItem : BaseEntity
    {

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Note { get; set; }
    }
}
