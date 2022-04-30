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
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Business.Concrete
{
    public class ShoppingCartItemManager : IShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ShoppingCartItemAddDto> _addValidator;

        public ShoppingCartItemManager(IShoppingCartItemRepository cartItemRepository, IMapper mapper, IValidator<ShoppingCartItemAddDto> addValidator)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _addValidator = addValidator;
        }

        public async Task<Result> AddAsync(ShoppingCartItemAddDto shoppingCartItemAddDto)
        {
            ValidationResult result = _addValidator.Validate(shoppingCartItemAddDto);
            if (result.IsValid)
            {
                ShoppingCartItem shoppingCartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemAddDto);
                await _cartItemRepository.AddAsync(shoppingCartItem);
                return Result.Success();
            }
            return Result.Failure("Validation Error", result.ConvertToCustomValidationErrors());
        }

        public async Task AddRangeAsync(List<ShoppingCartItem> shoppingCartItems)
        {
            await _cartItemRepository.AddRangeAsync(shoppingCartItems);
        }

        public async Task<DataResult<List<ShoppingCartItemGetDto>>> GetShoppingCartItems(int shoppingCartId)
        {
            List<ShoppingCartItem> shoppingCartItems = await _cartItemRepository.GetAllAsync(x => x.ShoppingCartId == shoppingCartId, true, x => x.Product, x => x.ShoppingCart);
            List<ShoppingCartItemGetDto> shoppingCartItemsToReturn = _mapper.Map<List<ShoppingCartItemGetDto>>(shoppingCartItems);
            return DataResult<List<ShoppingCartItemGetDto>>.Success(shoppingCartItemsToReturn);
        }

        public async Task<Result> ActivateAsync(int cartItemId)
        {
            ShoppingCartItem shoppingCart = await _cartItemRepository.GetAsync(x => x.Id == cartItemId);
            if (shoppingCart is null)
                return Result.Failure("Alışveriş Öğesi bulunamadı");
            shoppingCart.Status = true;
            await _cartItemRepository.Update(shoppingCart);
            return Result.Success("Sepet Başarıyla Güncelledi");
        }

        public async Task<Result> DectivateAsync(int cartItemId)
        {
            ShoppingCartItem shoppingCart = await _cartItemRepository.GetAsync(x => x.Id == cartItemId);
            if (shoppingCart is null)
                return Result.Failure("Alışveriş Öğesi bulunamadı");
            shoppingCart.Status = false;
            await _cartItemRepository.Update(shoppingCart);
            return Result.Success("Sepet Başarıyla Güncelledi");
        }

        public async Task<Result> HardDeleteAsync(int cartItemId)
        {
            ShoppingCartItem shoppingCart = await _cartItemRepository.GetAsync(x => x.Id == cartItemId);
            if (shoppingCart is null)
                return Result.Failure("Alışveriş Öğesi bulunamadı");
            await _cartItemRepository.HardDelete(shoppingCart);
            return Result.Success("Sepet Başarıyla Güncelledi");
        }

        public async Task<Result> HardDeleteAllAsync(int cartId)
        {
            List<ShoppingCartItem> shoppingCartItems = await _cartItemRepository.GetAllAsync(x => x.ShoppingCartId == cartId);
            await _cartItemRepository.DeleteRange(shoppingCartItems);
            return Result.Success();
        }

        public async Task<bool> CheckProductInList(int productId)
        {
            return await _cartItemRepository.AnyAsync(x => x.ProductId == productId);
        }
    }
}
