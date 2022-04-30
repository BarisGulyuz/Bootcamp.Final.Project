using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Business.Abstract;
using Teleperformance.Business.Extensions;
using Teleperformance.Core.Helpers.FileUpload;
using Teleperformance.Core.Utilities.Results;
using Teleperformance.DataAccess.Repositories.Abstract;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.Product;

namespace Teleperformance.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductAddDto> _addValidator;
        private readonly IValidator<ProductUpdateDto> _updateValidator;
        private readonly IShoppingCartItemService _shoppingCartItemService;

        public ProductManager(IProductRepository productRepository, IMapper mapper, IValidator<ProductAddDto> addValidator, IValidator<ProductUpdateDto> updateValidator, IShoppingCartItemService shoppingCartItemService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
            _shoppingCartItemService = shoppingCartItemService;
        }

        public async Task<Result> ActivateAsync(int productId)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product is null)
                return Result.Failure("Ürün Bulunamadı");
            product.Status = true;
            await _productRepository.Update(product);
            return Result.Success();
        }

        public async Task<Result> AddCAsync(ProductAddDto productAddDto)
        {
            List<CustomValidationError> errors = await ValidateCategory(productAddDto.Name);
            if (errors.Count > 0) return Result.Failure("", errors);

            ValidationResult result = _addValidator.Validate(productAddDto);
            if (result.IsValid)
            {
                Product product = _mapper.Map<Product>(productAddDto);
                CloudinaryHelper cloudinaryHelper = new();
                string imageUrl = cloudinaryHelper.AddPhotoAndGetUrl(productAddDto.Image);
                product.Photo = imageUrl;
                await _productRepository.AddAsync(product);
                return Result.Success("Ekleme İşlemi Başarılı");
            }
            return Result.Failure("Validation Error", result.ConvertToCustomValidationErrors());
        }

        public async Task<Result> DeleteAsync(int productId)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product != null)
            {
                await _productRepository.DeleteAsync(product);
                return Result.Success("Silme İşlemi Başarılı");
            }
            return Result.Failure("Silinecek ürün bulunamadı");
        }

        public async Task<DataResult<ProductGetDto>> GetProductAsync(int productId)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == productId, true, x => x.Category);
            if (product is null)
                DataResult<ProductGetDto>.Failure("Görüntülenecek ürün bulunamadı");
            ProductGetDto productToRetun = _mapper.Map<ProductGetDto>(product);
            return DataResult<ProductGetDto>.Success(productToRetun);
        }

        public async Task<DataResult<List<ProductGetDto>>> GetProductListAsync(int? categoryId, string searchByName)
        {

            IQueryable<Product> products = _productRepository.Query();
            //products = products.Where(x => x.Status);

            if (categoryId != 0) products = products.Where(x => x.CategoryId == categoryId);
            if (!string.IsNullOrEmpty(searchByName)) products = products.Where(x => x.Name.Contains(searchByName));
            products = products.Include(x => x.Category);

            List<ProductGetDto> productToReturn = _mapper.Map<List<ProductGetDto>>(await products.ToListAsync());
            return DataResult<List<ProductGetDto>>.Success(productToReturn);

        }

        public async Task<DataResult<List<ProductGetDto>>> GetProductsNotInShoppingList(int cartId)
        {

            IQueryable<Product> products = _productRepository.Query();
            products = products.Where(x => x.Status);

            var cartResult = await _shoppingCartItemService.GetShoppingCartItems(cartId);
            foreach (var item in cartResult.Data)
            {
                products = products.Where(x => x.Id != item.Product.Id);
            }
            products = products.Include(x => x.Category);
            List<ProductGetDto> productToReturn = _mapper.Map<List<ProductGetDto>>(await products.ToListAsync());
            return DataResult<List<ProductGetDto>>.Success(productToReturn);

        }

        public async Task<Result> HardDeleteAsync(int productId)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product != null)
            {
                bool productListStatus = await _shoppingCartItemService.CheckProductInList(productId);
                if (productListStatus) return Result.Failure("Ürün Bir Kullanıcının Listesinde Bulunduğu İçin Silinemiyor");
                await _productRepository.HardDelete(product);
                return Result.Success("Silme İşlemi Başarılı");
            }
            return Result.Failure("Silinecek ürün bulunamadı");
        }

        public async Task<Result> UpdateyAsync(ProductUpdateDto productUpdateDto)
        {
            List<CustomValidationError> errors = await ValidateCategory(productUpdateDto.Name);
            if (errors.Count > 0) return Result.Failure("", errors);

            ValidationResult result = _updateValidator.Validate(productUpdateDto);
            if (result.IsValid)
            {
                Product oldProduct = await _productRepository.GetAsync(x => x.Id == productUpdateDto.Id);
                if (oldProduct is null)
                    return Result.Failure("Görüntülenecek ürün bulunamadı");

                Product product = _mapper.Map<ProductUpdateDto, Product>(productUpdateDto, oldProduct);
                if (productUpdateDto.Image != null)
                {
                    CloudinaryHelper cloudinaryHelper = new();
                    string imageUrl = cloudinaryHelper.AddPhotoAndGetUrl(productUpdateDto.Image);
                    product.Photo = imageUrl;
                }
                await _productRepository.Update(product);
                return Result.Success();
            }
            return Result.Failure("Validation Error", result.ConvertToCustomValidationErrors());
        }


        private async Task<List<CustomValidationError>> ValidateCategory(string name)
        {
            List<CustomValidationError> validationErrors = new List<CustomValidationError>();
            if (await _productRepository.AnyAsync(x => x.Name == name))
            {
                validationErrors.Add(new CustomValidationError { PropertyName = "Kategori Adı", ErrorMessage = "Ürün Adı Benzersiz Olmalı, Bilgileri Kontrol Ediniz" });
            }
            return validationErrors;
        }
    }
}
