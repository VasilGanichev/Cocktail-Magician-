using CocktailMagicianWeb.Models;
using Microsoft.AspNetCore.Mvc;
using CocktailMagician.Services.Contracts;
using System.Threading.Tasks;
using CocktailMagicianWeb.Models.Ingredients;
using System.Linq;
using CocktailMagicianWeb.Utilities;
using Microsoft.AspNetCore.Authorization;
using CocktailMagicianWeb.Utilities.Mappers;

namespace CocktailMagicianWeb.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientServices _ingredientServices;
        private readonly ICocktailIngredientServices _cocktailIngredientServices;

        public IngredientsController(IIngredientServices ingredientServices, ICocktailIngredientServices cocktailIngredientServices)
        {
            _ingredientServices = ingredientServices;
            _cocktailIngredientServices = cocktailIngredientServices;
        }

        [HttpGet]
        [Authorize(Roles = "CocktailMagician")]
        public IActionResult ManageIngredients()
        {
            return View();
        }
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> ManageIngredients(IngredientsViewModel vm)
        {
            var viewModel = new IngredientsViewModel();
            var ingredients = await _ingredientServices.SearchIngredientsAsync(vm.Input);
            if (ingredients.Count() == 0)
            {
                ModelState.AddModelError(string.Empty, "No ingredients found with this name.");
                return View(viewModel);
            }

            viewModel.Ingredients = ingredients.Select(i => i.MapToViewModel()).ToList();
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "CocktailMagician")]
        public IActionResult AddIngredient()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> AddIngredient(AddIngredientViewModel vm)
        {
            await _ingredientServices.AddAsync(vm.Name, vm.Type);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> UpdateIngredient(int id)
        {
            var ingredient = await _ingredientServices.GetAsync(id);
            var vm = ingredient.MapToUpdateViewModel();
            return View(vm);
        }

        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> UpdateIngredient(UpdateIngredientViemModel vm)
        {
            await _ingredientServices.UpdateAsync(vm.ID, vm.NewName);
            return View("ManageIngredients");
        }

        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            if (await _ingredientServices.IsIngredientUsedAsync(id))
            {
                ModelState.AddModelError(string.Empty, "The ingredient is currently being used in a cokctail and thus cannot be deleted.");
                return View("ManageIngredients");
            }
            await _ingredientServices.DeleteAsync(id);
            return View("ManageIngredients");
        }

        public async Task RemoveIngredient(string cocktail, string ingredient)
        {
            await _cocktailIngredientServices.DeleteAsync(cocktail, ingredient);
        }

        public async Task<IActionResult> GetIngedientsByType(string type)
        {
            var ingredients = await _ingredientServices.GetIngedientsByTypeAsync(type);

            return Json(ingredients);
        }
    }
}