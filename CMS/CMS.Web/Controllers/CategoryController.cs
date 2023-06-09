﻿using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Infrastructure.Services.Categories;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetCategoryData(Pagination pagination, Query query)
        {
            var result = await _categoryService.GetAll(pagination, query);
            return Json(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Create(dto);
                return Ok(Resultss.AddSuccessResult());
            }

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _categoryService.Get(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(dto);
                return Ok(Resultss.EditSuccessResult());
            }

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return Ok(Resultss.DeleteSuccessResult());
        }
        //[HttpGet]
        //public IActionResult ExportToExcel()
        //{
        //    return View();
        //}
    }
}
