using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Utilities.Results;
using Teleperformance.Entities.DTOs.Category;

namespace Teleperformance.Business.Abstract
{
    public interface ICategoryService
    {
        Task<DataResult<List<CategoryGetDto>>> GetCategoryListAsync(string searchByName = null);
        Task<DataResult<CategoryGetDto>> GetCategoryAsync(int categoryId);
        Task<Result> AddCAsync(CategoryAddDto categoryAddDto);
        Task<Result> UpdateyAsync(CategoryUpdateDto categoryUpdateDto);
        Task<Result> DeleteAsync(int categoryId);
        Task<Result> HardDeleteAsync(int categoryId);
        Task<Result> ActivateAsync(int categoryId);

    }
}
