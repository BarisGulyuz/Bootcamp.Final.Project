using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Utilities.Results;
using Teleperformance.Entities.DTOs.Product;

namespace Teleperformance.Business.Abstract
{
    public interface IProductService
    {
        Task<DataResult<List<ProductGetDto>>> GetProductListAsync(int? categoryId, string searchByName);
        Task<DataResult<ProductGetDto>> GetProductAsync(int productId);
        Task<DataResult<List<ProductGetDto>>> GetProductsNotInShoppingList(int cartId);
        Task<Result> AddCAsync(ProductAddDto productAddDto);
        Task<Result> UpdateyAsync(ProductUpdateDto productUpdateDto);
        Task<Result> DeleteAsync(int productId);
        Task<Result> HardDeleteAsync(int productId);
        Task<Result> ActivateAsync(int productId);
    }
}
