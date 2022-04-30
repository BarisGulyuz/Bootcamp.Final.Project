using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
