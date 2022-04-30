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
using Teleperformance.Entities.DTOs.ShoppingCart;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Business.Concrete
{
    public class ShoppingCartManager : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IShoppingCartItemService _cartItemService;
        private readonly IMapper _mapper;
        private readonly IValidator<ShoppingCartAddDto> _addValidator;

        public ShoppingCartManager(IShoppingCartRepository cartRepository, IMapper mapper, IValidator<ShoppingCartAddDto> addValidator, IShoppingCartItemService cartItemService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _addValidator = addValidator;
            _cartItemService = cartItemService;
        }

        public async Task<Result> ActivateAsync(int shoppingCartId)
        {
            ShoppingCart shoppingCart = await _cartRepository.GetAsync(x => x.Id == shoppingCartId);
            if (shoppingCart is null)
                return Result.Failure("Alışveriş Sepeti bulunamadı");
            shoppingCart.IsShoppingStarted = false;
            await _cartRepository.Update(shoppingCart);
            return Result.Success();
        }

        public async Task<Result> AddAsync(ShoppingCartAddDto shoppingCartAddDto)
        {
            ValidationResult result = _addValidator.Validate(shoppingCartAddDto);
            if (result.IsValid)
            {
                ShoppingCart shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartAddDto);
                shoppingCart.IsShoppingStarted = false;
                await _cartRepository.AddAsync(shoppingCart);
                return Result.Success();
            }
            return Result.Failure("Validation Error", result.ConvertToCustomValidationErrors());
        }

        public async Task<Result> AddAsync(ShoppingCartAddDto shoppingCartAddDto, List<ShoppingCartItemAddDto> shoppingCartItems)
        {
            ValidationResult result = _addValidator.Validate(shoppingCartAddDto);
            if (result.IsValid)
            {
                ShoppingCart shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartAddDto);
                shoppingCart.ExpireDate = DateTime.Now.AddDays(30);
                await _cartRepository.AddAsync(shoppingCart);

                if (shoppingCartItems.Count > 0)
                {
                    List<ShoppingCartItem> shoppingCarts = new List<ShoppingCartItem>();
                    foreach (var item in shoppingCartItems)
                    {
                        shoppingCarts.Add(new ShoppingCartItem
                        {
                            ProductId = item.ProductId,
                            ShoppingCartId = shoppingCart.Id,
                            Note = item.Note,
                            Status = /*item.Status*/ true
                        });
                    }
                    await _cartItemService.AddRangeAsync(shoppingCarts);
                    return Result.Success();
                }
            }
            return Result.Failure("Validation Error", result.ConvertToCustomValidationErrors());
        }

        public async Task<Result> DeactiveAsync(int shoppingCartId)
        {
            ShoppingCart shoppingCart = await _cartRepository.GetAsync(x => x.Id == shoppingCartId);
            if (shoppingCart is null)
                return Result.Failure("Alışveriş Sepeti bulunamadı");
            shoppingCart.IsShoppingStarted = true;
            await _cartRepository.Update(shoppingCart);
            return Result.Success();
        }

        public async Task<DataResult<List<ShoppingCartGetDto>>> GetAllShoppingCartsAsync()
        {
            List<ShoppingCart> shoppingCarts = await _cartRepository.GetAllAsync(null, true, x => x.User, x => x.ShoppingCartItems);
            List<ShoppingCartGetDto> shoppingCartsToReturn = _mapper.Map<List<ShoppingCartGetDto>>(shoppingCarts);
            return DataResult<List<ShoppingCartGetDto>>.Success(shoppingCartsToReturn);
        }

        public async Task<DataResult<List<ShoppingCartGetDto>>> GetShoppingCartByUserAsync(int userId)
        {
            List<ShoppingCart> shoppingCarts = await _cartRepository.GetAllAsync(x => x.UserId == userId, false, x => x.User, x => x.ShoppingCartItems);
            List<ShoppingCartGetDto> shoppingCartsToReturn = _mapper.Map<List<ShoppingCartGetDto>>(shoppingCarts);
            return DataResult<List<ShoppingCartGetDto>>.Success(shoppingCartsToReturn);
        }

        public async Task<Result> RemoveAsync(int shoppingCartId)
        {
            ShoppingCart shoppingCart = await _cartRepository.GetAsync(x => x.Id == shoppingCartId);
            if (shoppingCart is null)
                return Result.Failure("Alışveriş Sepeti bulunamadı");
            await _cartRepository.HardDelete(shoppingCart);
            return Result.Success("Sepet Başarıyla Silindi");
        }
    }
}
