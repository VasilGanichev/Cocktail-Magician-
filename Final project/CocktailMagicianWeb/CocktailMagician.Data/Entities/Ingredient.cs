using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IngredientType Type { get; set; }
    }
}
