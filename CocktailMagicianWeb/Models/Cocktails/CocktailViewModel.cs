using CocktailMagician.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagicianWeb.Models.Cocktails
{
    public class CocktailViewModel
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public IFormFile NewPicture { get; set; }

        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }
        public bool  IsHidden { get; set; }
        public double Rating { get; set; }
        public List<CocktailReview> CocktailReviews { get; set; } = new List<CocktailReview>();

        [Required, MinLength(2)]
        public List<string> Ingredients { get; set; } = new List<string>(10);
        public List<string> Bars { get; set; } = new List<string>(20);
        [Required, MinLength(1)]
        public List<int> Quantities { get; set; } = new List<int>(10);
    }
}
