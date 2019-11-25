using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using CocktailMagician.Services.Contracts;
using CocktailMagicianWeb.Models.Cocktails;
using CocktailMagicianWeb.Utilities.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
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
        private readonly IBarServices _barServices;

        public CocktailsController(IIngredientServices ingredientServices, ICocktailServices cocktailServices, ICocktailIngredientServices cocktailIngredientsServices, IBarCocktailServices barCocktailServices, IBarServices barServices)
        {
            _ingredientServices = ingredientServices;
            _cocktailServices = cocktailServices;
            _cocktailIngredientsServices = cocktailIngredientsServices;
            _barCocktailServices = barCocktailServices;
            _barServices = barServices;
        }

        public async Task<IActionResult> ManageCocktails(CocktailsViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Input))
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
        public IActionResult AddCocktail()
        {
            return View();
        }

        public async Task<IActionResult> AddCocktail(CocktailViewModel vm, List<IFormFile> Picture)
        {
            foreach (var item in Picture)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        vm.Picture = stream.ToArray();
                    }
                }
            }
            var cocktail = await _cocktailServices.AddAsync(vm.Name, vm.Picture);
            var ingredients = await _ingredientServices.GetMultipleIngredientsByNameAsync(vm.Ingredients);
            for (int i = 0; i < vm.Quantities.Count; i++)
            {
                await _cocktailIngredientsServices.AddAsync(cocktail, ingredients[i], vm.Quantities[i]);
            }

            foreach (var bar in vm.Bars)
            {
                var barEntity = await _barServices.GetAsync(bar);
                await _barCocktailServices.CreateAsync(barEntity, cocktail);
            }
            return RedirectToAction("UpdateCocktail", new { id = cocktail.Id });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCocktail(int id)
        {
            var cocktail = await _cocktailServices.GetAsync(id);
            var allBars = await _barServices.GetCollectionAsync();
            var vm = cocktail.MapToViewUpdateModel(allBars);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCocktail(UpdateCocktailViewModel vm)
        {
            var cocktail = await vm.MapToEntity();
            await _cocktailServices.UpdateCocktailAsync(cocktail);
            var currentIngredients = vm.CurrentIngredients.Select(c => c.MapToEntity()).ToList();
            for (int i = 0; i < currentIngredients.Count; i++)
            {
                if (await _cocktailIngredientsServices.IsPairUpdatedAsync(currentIngredients[i], cocktail, vm.Quantities[i]))
                {
                    if (vm.Quantities[i] == 0)
                    {
                        continue;
                    }

                    await _cocktailIngredientsServices.UpdateAsync(currentIngredients[i], cocktail, vm.Quantities[i]);
                }
            }

            var newIngredients = await _ingredientServices.GetMultipleIngredientsByNameAsync(vm.Ingredients);
            for (int i = 0; i < newIngredients.Count; i++)
            {
                if (!await _cocktailIngredientsServices.PairExistsAsync(newIngredients[i], cocktail))
                {
                    // Update
                    if (vm.Quantities[i] == 0)
                    {
                        continue;
                    }

                    await _cocktailIngredientsServices.AddAsync(cocktail, newIngredients[i], vm.Quantities[i]);
                }
            }

            return View("ManageCocktails");
        }

        public async Task HideCocktail([FromBody]CocktailViewModel vm)
        {
            await _cocktailServices.HideAsync(vm.Id);
        }

        public async Task UnhideCocktail([FromBody]CocktailViewModel vm)
        {
            await _cocktailServices.UnhideAsync(vm.Id);
        }

        public async Task UpdateBarCocktailPairs(string cocktailName, string[] currentlyCheckedBars)
        {
            var cocktail = await _cocktailServices.GetAsync(cocktailName);
            var originalOfferingBars = cocktail.Bars.Select(b => b.Bar).ToList();
            var userCheckBars = new List<Bar>(100);
            foreach (var barName in currentlyCheckedBars)
            {
                var bar = await _barServices.GetAsync(barName);
                userCheckBars.Add(bar);
            }

            var pairsToDelete = originalOfferingBars.Except(userCheckBars);
            var newPairs = userCheckBars.Except(originalOfferingBars);
            foreach (var bar in newPairs)
            {
                await _barCocktailServices.CreateAsync(bar, cocktail);
            }

            foreach (var bar in pairsToDelete)
            {
                await _barCocktailServices.DeleteAsync(bar, cocktail);
            }
        }
        public async Task<IActionResult> GetCocktails()
        {
            var cocktails = await _cocktailServices.GetCollectionAsync();
            return Json(cocktails);
        }
        [HttpGet]
        public IActionResult SearchCocktails()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchCocktails(SearchCocktailViewModel viewModel)
        {
            if (viewModel.AvgRating != null)
            {
                viewModel.SearchResults = (await _cocktailServices.SearchByMultipleCriteriaAsync(viewModel.Name, viewModel.Ingredient, viewModel.IncludeOnlyAlcoholicDrinks)).Select(c => c.MapToViewModel()).Where(b => b.Rating == viewModel.AvgRating).ToList();
            }
            else
            {
                viewModel.SearchResults = (await _cocktailServices.SearchByMultipleCriteriaAsync(viewModel.Name, viewModel.Ingredient, viewModel.IncludeOnlyAlcoholicDrinks)).Select(c => c.MapToViewModel()).ToList();
            }
            return View(viewModel);
        }
        public async Task<IActionResult> CocktailDetails(int id)
        {
            var cocktailModel = (await _cocktailServices.GetAsync(id)).MapToViewModel();
            return View(cocktailModel);

        }
        public async Task<IActionResult> LoadMoreBars([FromQuery]int Loaded, [FromQuery]int id)
        {
            var bars = await _cocktailServices.LoadMoreBars(Loaded, id);
            return Json(bars);
        }
    }
}