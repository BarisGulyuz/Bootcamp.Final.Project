using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teleperformance.Business.Abstract;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.Category;
using Teleperformance.Entities.DTOs.ShoppingCart;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.UI.Controllers
{
    [Authorize(Roles = "Member")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<User> _userManager;

        public HomeController(IProductService productService, IShoppingCartService shoppingCartService, UserManager<User> userManager, ICategoryService categoryService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(bool timeEnd = false)
        {
            User user = _userManager.GetUserAsync(HttpContext.User).Result;
            //if timeEnd false olurs bu çalışsın
            var result = await _shoppingCartService.GetShoppingCartByUserAsync(user.Id);
            if (timeEnd)
            {
                //Bussiness'a method yaz
                List<ShoppingCartGetDto> shoppingCartGetDtos = result.Data.Where(x => x.ExpireDate <= DateTime.Now).ToList();
                return View(shoppingCartGetDtos);
            }
            var categoryResult = await _categoryService.GetCategoryListAsync();

            ViewBag.Categories = new SelectList(categoryResult.Data, "Name", "Name"); //js kullanarak arama için böyle yaptım

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int categoryId, string name)
        {
            var result = await _productService.GetProductListAsync(categoryId, name);
            return Json(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsNotInList(int cartId)
        {
            var cartResult = await _productService.GetProductsNotInShoppingList(cartId);
            return Json(cartResult.Data);

        }


    }
}
