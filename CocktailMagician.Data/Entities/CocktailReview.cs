using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class CocktailReview
    {
        public int Id { get; set; }
        [Range(0, 5)]
        public double Rating { get; set; }
        [MaxLength(300)]
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
