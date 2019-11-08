using CocktailMagician.Data.Entities;
using CocktailMagicianWeb.Models.Ingredients;

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
    }
}
