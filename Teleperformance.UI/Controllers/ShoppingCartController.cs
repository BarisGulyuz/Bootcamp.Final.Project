using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Teleperformance.Business.Abstract;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.ShoppingCart;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.UI.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IShoppingCartItemService shoppingCartItemService, IMapper mapper, UserManager<User> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartItemService = shoppingCartItemService;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetListItems(int cartId)
        {
            var result = await _shoppingCartItemService.GetShoppingCartItems(cartId);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ExcelReport(int cartId)
        {
            var result = await _shoppingCartItemService.GetShoppingCartItems(cartId);
            if (result.IsSuccess)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Alışveriş Listesi");
                    worksheet.Cell(1, 1).Value = "Ürün Adı";
                    worksheet.Cell(1, 2).Value = "Not";

                    int blogRowCount = 2;
                    foreach (var item in result.Data)
                    {
                        worksheet.Cell(blogRowCount, 1).Value = item.Product.Name;
                        worksheet.Cell(blogRowCount, 2).Value = item.Note;
                        blogRowCount++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreads.sheet", "Alışveriş_Listesi.xlsx");
                    }
                }
            }
            TempData.Add("ErrorMessage", result.Message);
            return RedirectToAction("GetListItems", new { cartId = cartId });

        }

        [HttpPost]
        public async Task<IActionResult> AddList(string title, List<ShoppingCartItemAddDto> shoppingCartItemAddDtos)
        {
            User user = _userManager.GetUserAsync(HttpContext.User).Result;
            ShoppingCartAddDto shoppingCartAddDto = new ShoppingCartAddDto
            {
                Title = title,
                UserId = user.Id,
                Status = true
            };
            var result = await _shoppingCartService.AddAsync(shoppingCartAddDto, shoppingCartItemAddDtos);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddListWithOutTitle(List<ShoppingCartItemAddDto> shoppingCartItemAddDtos)
        {
            List<ShoppingCartItem> shoppingCartItems = _mapper.Map<List<ShoppingCartItem>>(shoppingCartItemAddDtos);
            await _shoppingCartItemService.AddRangeAsync(shoppingCartItems);
            return Json("Success");

        }

        [HttpGet]
        public async Task<IActionResult> SetShoppingStatus(int cartId)
        {
            var result = await _shoppingCartService.DeactiveAsync(cartId);
            if (result.IsSuccess)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData.Add("ErrorMessage", "Silinecek Liste Bulunamadı");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCartItem(int cartId, int cartItemId)
        {
            var result = await _shoppingCartItemService.HardDeleteAsync(cartItemId);
            if (result.IsSuccess)
                return RedirectToAction("GetListItems", new { cartId = cartId });
            else
            {
                TempData.Add("ErrorMessage", "Silinecek Liste Bulunamadı");
                return RedirectToAction("GetListItems", new { cartId = cartId });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SetCartItemStatus(bool status, int cartItemId, int cartId)
        {
            if (status == true)
            {
                await _shoppingCartItemService.DectivateAsync(cartItemId);
                return RedirectToAction("GetListItems", new { cartId = cartId });
            }
            else
            {
                await _shoppingCartItemService.ActivateAsync(cartItemId);
                return RedirectToAction("GetListItems", new { cartId = cartId });

            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            var result = await _shoppingCartService.RemoveAsync(cartId);
            if (result.IsSuccess)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData.Add("ErrorMessage", "Silinecek Liste Bulunamadı");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllItems(int cartId)
        {
            var result = await _shoppingCartItemService.HardDeleteAllAsync(cartId);
            if (result.IsSuccess)
            {
                await _shoppingCartService.ActivateAsync(cartId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData.Add("ErrorMessage", "Ürünler silinirken bir sorun oluştu");
                return RedirectToAction("Index", "Home");
            }
        }

    
    }
}
