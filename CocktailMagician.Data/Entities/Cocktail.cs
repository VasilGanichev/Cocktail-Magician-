using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class Cocktail
    {   
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }
        public ICollection<CocktailIngredient> Ingredients { get; set; }
        public ICollection<BarCocktail> Bars { get; set; }
        public bool IsHidden { get; set; }
        public int CocktailsReviewID { get; set; }
        public List<CocktailReview> CocktailReviews { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
