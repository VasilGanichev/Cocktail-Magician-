using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagicianWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagicianWeb.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LeaveBarReview(int Id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LeaveBarReview(ReviewViewModel viewModel)
        {
            return View();
        }
    }
}