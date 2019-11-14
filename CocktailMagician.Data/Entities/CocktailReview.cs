using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class CocktailReview
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
