using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.DataAccess.Repository;
using Teleperformance.Entities.Concrete;

namespace Teleperformance.DataAccess.Repositories.Abstract
{
    public interface IShoppingCartItemRepository : IBaseRepository<ShoppingCartItem>
    {
    }
}
