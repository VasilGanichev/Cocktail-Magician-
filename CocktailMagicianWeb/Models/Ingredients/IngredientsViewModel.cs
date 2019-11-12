using System.Collections.Generic;

namespace CocktailMagicianWeb.Models.Ingredients
{
    public class IngredientsViewModel
    {
        public string Input { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; } = new List<IngredientViewModel>();
    }
}
