﻿using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetUserData(Pagination pagination, Query query)
        {
            var result = await _userService.GetAll(pagination, query);
            return Json(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserDto dto)
        {
            if (ModelState.IsValid)
            {
                await _userService.Create(dto);
                return Ok(Resultss.AddSuccessResult());
            }

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userService.Get(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateUserDto dto)
        {
            if (ModelState.IsValid)
            {
                await _userService.Update(dto);
                return Ok(Resultss.EditSuccessResult());
            }

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.Delete(id);
            return Ok(Resultss.DeleteSuccessResult());
        }
        //[HttpGet]
        //public IActionResult ExportToExcel()
        //{
        //    return View();
        //}
    }
}
