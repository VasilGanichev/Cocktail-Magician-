using CocktailMagician.Data.Entities;
using CocktailMagicianWeb.Models.Ingredients;
using CocktailMagicianWeb.Models.Cocktails;
using System.Linq;
using CocktailMagicianWeb.Utilities.Mappers;

namespace CocktailMagicianWeb.Utilities
{
    public static class MapperExtensions
    {
        public static IngredientViewModel MapToViewModel(this Ingredient ingredient)
        {
            var vm = new IngredientViewModel();
            vm.ID = ingredient.Id;
            vm.Name = ingredient.Name;
            vm.Type = ingredient.Type;
            return vm;
        }
        public static UpdateIngredientViemModel MapToUpdateViewModel(this Ingredient ingredient)
        {
            var vm = new UpdateIngredientViemModel();
            vm.ID = ingredient.Id;
            vm.CurrentName = ingredient.Name;
            return vm;
        }

        public static CocktailViewModel MapToViewModel(this Cocktail cocktail)
        {
            var vm = new CocktailViewModel();
            vm.Id = cocktail.Id;
            vm.Name = cocktail.Name;
            vm.IsHidden = cocktail.IsHidden;
            vm.Picture = cocktail.Picture;
            return vm;
        }

        public static UpdateCocktailViewModel MapToViewUpdateModel(this Cocktail cocktail)
        {
            var vm = new UpdateCocktailViewModel();
            vm.Id = cocktail.Id;
            vm.Name = cocktail.Name;
            vm.IsHidden = cocktail.IsHidden;
            vm.CurrentPicture = cocktail.Picture;
            vm.Picture = cocktail.Picture;
            vm.Quantities = cocktail.Ingredients.Select(i => i.Quantity).ToList();
            vm.Ingredients = cocktail.Ingredients.Select(c => c.Ingredient.MapToViewModel()).ToList();
            vm.Bars = cocktail.Bars.Select(c => c.Bar.MapToViewModel()).ToList();
            return vm;
        }

        public static Cocktail MapToEntity(this UpdateCocktailViewModel vm)
        {
            var cocktail = new Cocktail
            {
                Id = vm.Id,
                Name = vm.Name,
                Picture = vm.Picture,
                IsHidden = vm.IsHidden
            };
            if (cocktail.Picture == null)
            {
                cocktail.Picture = vm.CurrentPicture;
            }

            return cocktail;
        }

        public static Ingredient MapToEntity(this IngredientViewModel vm)
        {
            var ingredient = new Ingredient
            {
                Id = vm.ID,
                Name = vm.Name,
                Type = vm.Type
            };

            return ingredient;
        }
    }
}
