using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using CocktailMagicianWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagicianWeb.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IBarServices _barServices;
        private readonly IReviewServices _reviewServices;
        private readonly UserManager<User> _userManager;

        public ReviewsController(IBarServices barServices, IReviewServices reviewServices, UserManager<User> userManager)
        {
            _barServices = barServices;
            _reviewServices = reviewServices;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LeaveBarReview(int Id)
        {
            var viewModel = new ReviewViewModel
            {
                Id = Id,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LeaveBarReview(ReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var bar = await _barServices.GetBarAsync(viewModel.Id);
            await _reviewServices.CreateReviewAsync(viewModel.Rating, viewModel.Comment, bar, null, user);
            return RedirectToAction("Index", "Home");
        }
    }
}