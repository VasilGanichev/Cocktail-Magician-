using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using CocktailMagicianWeb.Models.Cocktails;
using CocktailMagicianWeb.Utilities;
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
            this._barServices = barServices;
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
            for (int i = 0; i < ingredients.Count; i++)
            {
                await _cocktailIngredientsServices.AddAsync(cocktail, ingredients[i], vm.Quantities[i]);
            }
            var bars = await _barServices.GetCollectionAsync();
            if (bars != null)
            {
                foreach (var bar in bars)
                {
                    await _barCocktailServices.CreateAsync(bar, cocktail);
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCocktail(int id)
        {
            var cocktail = await _cocktailServices.GetByIdAsync(id);
            var vm = cocktail.MapToViewUpdateModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCocktail(UpdateCocktailViewModel vm)
        {
            var cocktail = vm.MapToEntity();
            await _cocktailServices.UpdateCocktail(cocktail);
            var ingredients = vm.Ingredients.Select(i => i.MapToEntity()).ToList();
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (! await _cocktailIngredientsServices.PairExists(ingredients[i], cocktail))
                {
                    await _cocktailIngredientsServices.AddAsync(cocktail, ingredients[i], vm.Quantities[i]);
                }
            }
            return View(vm);
        }

        //public async Task<IActionResult> UpdateCocktail(int id)
        //{
        //    var bars = await _barServices.GetCollectionAsync();
        //    return Json(bars);
        //}

        public async Task HideCocktail([FromBody]CocktailViewModel vm)
        {
            await _cocktailServices.HideAsync(vm.Id);
        }

        public async Task UnhideCocktail([FromBody]CocktailViewModel vm)
        {
            await _cocktailServices.UnhideAsync(vm.Id);
        }

        public async Task<IActionResult> GetIngedientsByType(string type)
        {
            var ingredients = await _ingredientServices.GetIngedientsByTypeAsync(type);

            return Json(ingredients);
        }
    }
}