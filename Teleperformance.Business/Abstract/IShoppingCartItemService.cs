using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Utilities.Results;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Business.Abstract
{
    public interface IShoppingCartItemService
    {
        Task<DataResult<List<ShoppingCartItemGetDto>>> GetShoppingCartItems(int shoppingCartId);
        Task<Result> AddAsync(ShoppingCartItemAddDto shoppingCartItemAddDto);
        Task AddRangeAsync(List<ShoppingCartItem> shoppingCartItems);
        Task<Result> ActivateAsync(int cartItemId);
        Task<Result> DectivateAsync(int cartItemId);
        Task<Result> HardDeleteAsync(int cartItemId);
        Task<Result> HardDeleteAllAsync(int cartId);
        Task<bool> CheckProductInList(int productId);

    }
}
