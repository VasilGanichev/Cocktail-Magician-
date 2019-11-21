using CocktailMagician.Data.Entities;
using CocktailMagicianWeb.Models.Ingredients;
using CocktailMagicianWeb.Models.Cocktails;
using System;
using System.Linq;

namespace CocktailMagicianWeb.Utilities
{
    public static class MapperExtensions
    {
        public static IngredientViewModel MapToViewModel(this Ingredient ingredient)
        {
            var vm = new IngredientViewModel();
            vm.ID = ingredient.ID;
            vm.Name = ingredient.Name;
            vm.Type = ingredient.Type;
            return vm;
        }
        public static UpdateIngredientViemModel MapToUpdateViewModel(this Ingredient ingredient)
        {
            var vm = new UpdateIngredientViemModel();
            vm.ID = ingredient.ID;
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
            try
            {
                vm.Rating = cocktail.CocktailReviews.Select(b => b.Rating).Average();
            }
            catch (Exception)
            {
                vm.Rating = 0;
            }
            return vm;
        }
    }
}
