using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using CocktailMagicianWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Controllers
{
    [Authorize(Roles = "BarCrawler, CocktailMagician")]
    public class ReviewsController : Controller
    {
        private readonly IBarServices _barServices;
        private readonly IBarReviewServices _barReviewServices;
        private readonly ICocktailReviewServices _cocktailReviewServices;
        private readonly ICocktailServices _cocktailServices;
        private readonly UserManager<User> _userManager;

        public ReviewsController(IBarServices barServices, IBarReviewServices barReviewServices, ICocktailReviewServices cocktailReviewServices, ICocktailServices cocktailServices, UserManager<User> userManager)
        {
            _barServices = barServices;
            _barReviewServices = barReviewServices;
            _cocktailReviewServices = cocktailReviewServices;
            _cocktailServices = cocktailServices;
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
            await _barReviewServices.CreateBarReviewAsync(viewModel.Rating, viewModel.Comment, bar, user);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LoadBarReviews(int id)
        {
            var reviews = await _barReviewServices.GetBarReviewsCollectionAsync(id);
            return PartialView("_LoadedBarReviewsPartial", reviews);
        }
        [HttpGet]
        public IActionResult LeaveCocktailReview(int Id)
        {
            var viewModel = new ReviewViewModel
            {
                Id = Id,
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> LeaveCocktailReview(ReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var cocktail = await _cocktailServices.GetAsync(viewModel.Id);
            await _cocktailReviewServices.CreateCocktailReviewAsync(viewModel.Rating, viewModel.Comment, cocktail, user);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LoadCoctailReviews(int id)
        {
            var cocktailReviews = await _cocktailReviewServices.GetCocktailReviewsCollectionAsync(id);
            return PartialView("_LoadedCocktailReviewsPartial", cocktailReviews);
        }
    }
}