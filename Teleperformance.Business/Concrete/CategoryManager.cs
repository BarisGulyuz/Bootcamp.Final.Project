using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Business.Abstract;
using Teleperformance.Business.Extensions;
using Teleperformance.Core.Utilities.Results;
using Teleperformance.DataAccess.Repositories.Abstract;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.Category;

namespace Teleperformance.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryAddDto> _addValidator;
        private readonly IValidator<CategoryUpdateDto> _updateValidator;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper, IValidator<CategoryAddDto> addValidator, IValidator<CategoryUpdateDto> updateValidator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Result> ActivateAsync(int categoryId)
        {
            Category category = await _categoryRepository.GetAsync(x => x.Id == categoryId);
            if (category is null)
                return Result.Failure("Kategori Bulunamadı");
            category.Status = true;
            await _categoryRepository.Update(category);
            return Result.Success();
        }

        public async Task<Result> AddCAsync(CategoryAddDto categoryAddDto)
        {
            List<CustomValidationError> errors = await ValidateCategory(categoryAddDto.Name);
            if (errors.Count > 0) return Result.Failure("", errors);

            ValidationResult result = _addValidator.Validate(categoryAddDto);
            if (result.IsValid)
            {
                Category category = _mapper.Map<Category>(categoryAddDto);
                await _categoryRepository.AddAsync(category);
                return Result.Success("Ekleme İşlemi Başarılı");
            }
            return Result.Failure("Validation Error", result.ConvertToCustomValidationErrors());
        }

        public async Task<Result> DeleteAsync(int categoryId)
        {
            Category category = await _categoryRepository.GetAsync(x => x.Id == categoryId);
            if (category is null)
                return Result.Failure("Kategori Bulunamadı");
            await _categoryRepository.DeleteAsync(category);
            return Result.Success("Silme İşlemi Başarılı");
        }

        public async Task<DataResult<CategoryGetDto>> GetCategoryAsync(int categoryId)
        {
            Category category = await _categoryRepository.GetAsync(x => x.Id == categoryId);
            if (category is null)
                return DataResult<CategoryGetDto>.Failure("Category Is Not Found");

            CategoryGetDto categoryToReturn = _mapper.Map<CategoryGetDto>(category);
            return DataResult<CategoryGetDto>.Success(categoryToReturn);
        }

        public async Task<DataResult<List<CategoryGetDto>>> GetCategoryListAsync(string searchByName = null)
        {
            List<Category> categories;
            if (searchByName == null) categories = await _categoryRepository.GetAllAsync(tracking: false);
            else categories = await _categoryRepository.GetAllAsync(x => x.Name.Contains(searchByName), tracking: false);

            List<CategoryGetDto> categoriesToReturn = _mapper.Map<List<CategoryGetDto>>(categories);
            return DataResult<List<CategoryGetDto>>.Success(categoriesToReturn);
        }

        public async Task<Result> HardDeleteAsync(int categoryId)
        {
            Category category = await _categoryRepository.GetAsync(x => x.Id == categoryId);
            if (category is null)
                return Result.Failure("Category Is Not Found");
            //ToDo: kategoriye bağlı product varsa silmeme kodunu yaz
            await _categoryRepository.HardDelete(category);
            return Result.Success("Silme İşlemi Başarılı");

        }


        public async Task<Result> UpdateyAsync(CategoryUpdateDto categoryUpdateDto)
        {
            List<CustomValidationError> errors = await ValidateCategory(categoryUpdateDto.Name);
            if (errors.Count > 0) return Result.Failure("", errors);

            ValidationResult result = _updateValidator.Validate(categoryUpdateDto);
            if (result.IsValid)
            {
                Category oldCategory = await _categoryRepository.GetAsync(x => x.Id == categoryUpdateDto.Id);
                if (oldCategory is null)
                    return Result.Failure("Category Is Not Found");

                Category category = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
                await _categoryRepository.Update(category);
                return Result.Success();
            }
            return Result.Failure("Validation Error", result.ConvertToCustomValidationErrors());
        }

        private async Task<List<CustomValidationError>> ValidateCategory(string name)
        {
            List<CustomValidationError> validationErrors = new List<CustomValidationError>();
            if (await _categoryRepository.AnyAsync(x => x.Name == name))
            {
                validationErrors.Add(new CustomValidationError { PropertyName = "Kategori Adı", ErrorMessage = "Kategori Adı Benzersiz Olmalı. Bilgileri Kontrol Ediniz" });
            }
            return validationErrors;
        }
    }
}
