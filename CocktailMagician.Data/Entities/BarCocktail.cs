﻿namespace CocktailMagician.Data.Entities
{
    public class BarCocktail
    {
        public int Id { get; set; }
        public int BarID { get; set; }
        public Bar Bar { get; set; }
        public int CocktailID { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
