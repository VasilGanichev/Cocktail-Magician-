using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class Cocktail
    {   
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string Name { get; set; }
        public ICollection<CocktailIngredient> Ingredients { get; set; }
        public ICollection<BarCocktail> Bars { get; set; }
    }
}
