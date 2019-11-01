using System.Collections.Generic;

namespace CocktailMagician.Data.Entities
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
    }
}
