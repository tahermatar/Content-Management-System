﻿using Microsoft.AspNetCore.Mvc;
namespace CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}