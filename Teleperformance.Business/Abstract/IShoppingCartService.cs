using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Utilities.Results;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.ShoppingCart;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Business.Abstract
{
    public interface IShoppingCartService
    {
        Task<DataResult<List<ShoppingCartGetDto>>> GetAllShoppingCartsAsync();
        Task<DataResult<List<ShoppingCartGetDto>>> GetShoppingCartByUserAsync(int userId);
        Task<Result> AddAsync(ShoppingCartAddDto shoppingCartAddDto);
        Task<Result> AddAsync(ShoppingCartAddDto shoppingCartAddDto, List<ShoppingCartItemAddDto> shoppingCartItems);
        Task<Result> RemoveAsync(int shoppingCartId);
        Task<Result> DeactiveAsync(int shoppingCartId);
        Task<Result> ActivateAsync(int shoppingCartId);
    }
}
