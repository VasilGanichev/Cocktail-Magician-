using CocktailMagician.Services.Contracts;
using CocktailMagicianWeb.Models.Cocktails;
using CocktailMagicianWeb.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly IIngredientServices _ingredientServices;
        private readonly ICocktailServices _cocktailServices;
        private readonly ICocktailIngredientServices _cocktailIngredientsServices;
        private readonly IBarCocktailServices _barCocktailServices;

        public CocktailsController(IIngredientServices ingredientServices, ICocktailServices cocktailServices, ICocktailIngredientServices cocktailIngredientsServices, IBarCocktailServices barCocktailServices)
        {
           _ingredientServices = ingredientServices;
           _cocktailServices = cocktailServices;
           _cocktailIngredientsServices = cocktailIngredientsServices;
           _barCocktailServices = barCocktailServices;
        }
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> ManageCocktails(CocktailsViewModel vm)
        {
            if (vm is null)
            {
                return View();
            }
            var viewModel = new CocktailsViewModel();
            var cocktails = await _cocktailServices.GetMultipleCocktailsByNameAsync(vm.Input);
            if (cocktails.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "No cocktails found with this name.");
                return View(viewModel);
            }

            viewModel.Cocktails = cocktails.Select(c => c.MapToViewModel()).ToList();
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "CocktailMagician")]
        public IActionResult AddCocktail()
        {
            return View();
        }
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> AddCocktail(CocktailViewModel vm)
        {
            var cocktail = await _cocktailServices.AddAsync(vm.Name, vm.Picture);
            var ingredients = await _ingredientServices.GetMultipleIngredientsByNameAsync(vm.Ingredients);
            for (int i = 0; i < ingredients.Count; i++)
            {
                await _cocktailIngredientsServices.AddAsync(cocktail, ingredients[i], vm.Quantities[i]);
            }
            var bars = await _cocktailServices.GetCollectionAsync();
            if (bars != null)
            {
                foreach (var bar in bars)
                {
                    await _barCocktailServices.CreateAsync(bar, cocktail);
                }
            }
            return View();
        }
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> GetIngedientsByType(string type)
        {
            var ingredients = await _ingredientServices.GetIngedientsByTypeAsync(type);
            return Json(ingredients);
        }
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> GetBars(string type)
        {
            var bars = await _cocktailServices.GetCollectionAsync();
            return Json(bars);
        }
    }
}