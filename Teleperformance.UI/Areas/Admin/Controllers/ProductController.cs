using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teleperformance.Business.Abstract;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.Product;
using Teleperformance.UI.Extensions;

namespace Teleperformance.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int categoryId, string name)
        {
            var result = await _productService.GetProductListAsync(categoryId, name);
            ViewBag.Name = name;
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categoryResult = await _categoryService.GetCategoryListAsync();
            ViewBag.Categories = new SelectList(categoryResult.Data, "Id", "Name");
            return View("_ProductAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            var result = await _productService.AddCAsync(productAddDto);
            if (result.IsSuccess)
            {
                TempData.Add("SuccessMessage", result.Message);
                return Json(new { IsValid = true, Message = result.Message });
            }

            ModelState.AddCustomErros(result.ValidationErrors);
            return Json(new { IsValid = false, Errors = ModelState.Values.SelectMany(x => x.Errors).ToList() });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int productId)
        {
            var categoryResult = await _categoryService.GetCategoryListAsync();
            ViewBag.Categories = new SelectList(categoryResult.Data, "Id", "Name");

            var result = await _productService.GetProductAsync(productId);
            return View("_ProductUpdatePartial", new ProductUpdateDto
            {
                Id = result.Data.Id,
                CategoryId = result.Data.CategoryId,
                Name = result.Data.Name,
                Status = result.Data.Status
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var result = await _productService.UpdateyAsync(productUpdateDto);
            if (result.IsSuccess)
                return Json(new { IsValid = true, Message = result.Message });

            ModelState.AddCustomErros(result.ValidationErrors);
            return Json(new { IsValid = false, Errors = ModelState.Values.SelectMany(x => x.Errors).ToList()});
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var productResult = await _productService.GetProductAsync(productId);
            if (productResult.IsSuccess)
            {
                if (productResult.Data.Status == true)
                {
                    var result = await _productService.DeleteAsync(productId);
                    if (result.IsSuccess)
                    {
                        TempData.Add("ErrorMessage", result.Message);
                        return RedirectToAction(nameof(Index));
                    }
                    TempData.Add("ErrorMessage", result.Message);
                }
                else
                {
                    var result = await _productService.HardDeleteAsync(productId);
                    if (result.IsSuccess)
                    {
                        TempData.Add("ErrorMessage", "Silme İşlemi Başarılı");
                        return RedirectToAction(nameof(Index));
                    }
                    TempData.Add("ErrorMessage", result.Message);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Activate(int productId)
        {
            var result = await _productService.ActivateAsync(productId);
            if (result.IsSuccess)
            {
                TempData.Add("SuccessMessage", "Aktif Etme İşlemi Başarılı");
                return RedirectToAction("Index");
            }
            TempData.Add("ErrorMessage", result.Message);
            return RedirectToAction("Index");
        }
    }
}
