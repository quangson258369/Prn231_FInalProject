﻿using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UnitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
