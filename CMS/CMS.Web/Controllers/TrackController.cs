﻿using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Infrastructure.Services.Categories;
using CMS.Infrastructure.Services.Tracks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Web.Controllers
{
    public class TrackController : Controller
    {
        private ITrackService _trackService;
        private ICategoryService _categoryService;
        public TrackController(ITrackService trackService, ICategoryService categoryService)
        {
            _trackService = trackService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetTrackData(Pagination pagination, Query query)
        {
            var result = await _trackService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTrackDto dto)
        {
            if (ModelState.IsValid)
            {
                await _trackService.Create(dto);
                return Ok(Resultss.AddSuccessResult());
            }

            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var track = await _trackService.Get(id);
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            return View(track);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTrackDto dto)
        {
            if (ModelState.IsValid)
            {
                await _trackService.Update(dto);
                return Ok(Resultss.EditSuccessResult());
            }

            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _trackService.Delete(id);
            return Ok(Resultss.DeleteSuccessResult());
        }
        //[HttpGet]
        //public IActionResult ExportToExcel()
        //{
        //    return View();
        //}
    }
}
