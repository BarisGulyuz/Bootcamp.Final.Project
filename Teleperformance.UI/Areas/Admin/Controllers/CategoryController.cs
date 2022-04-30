using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teleperformance.Business.Abstract;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.Category;
using Teleperformance.UI.Extensions;

namespace Teleperformance.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name)
        {
            var result = await _categoryService.GetCategoryListAsync(name);
            ViewBag.Name = name;
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("_CategoryAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            var result = await _categoryService.AddCAsync(categoryAddDto);
            if (result.IsSuccess)
            {
                TempData.Add("SuccessMessage", result.Message);
                return Json(new { IsValid = true, Message = result.Message });
            }

            ModelState.AddCustomErros(result.ValidationErrors);
            return Json(new { IsValid = false, Errors = ModelState.Values.SelectMany(x => x.Errors).ToList() });

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var categegoryResult = await _categoryService.GetCategoryAsync(categoryId);
            if (categegoryResult.IsSuccess)
            {
                if (categegoryResult.Data.Status == true)
                {
                    var result = await _categoryService.DeleteAsync(categoryId);
                    if (result.IsSuccess)
                    {
                        TempData.Add("ErrorMessage", result.Message);
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    var result = await _categoryService.HardDeleteAsync(categoryId);
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
        public async Task<IActionResult> Activate(int categoryId)
        {
            var result = await _categoryService.ActivateAsync(categoryId);
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
