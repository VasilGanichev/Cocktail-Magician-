using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class CocktailIngredient
    {
        public int ID { get; set; }
        public int CocktailID { get; set; }
        public Cocktail Cocktail { get; set; }
        public int IngredienetID { get; set; }
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}
