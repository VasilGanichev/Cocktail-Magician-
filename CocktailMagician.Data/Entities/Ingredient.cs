using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
    }
}
