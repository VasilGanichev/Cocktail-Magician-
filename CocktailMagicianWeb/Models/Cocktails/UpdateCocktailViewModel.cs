using CocktailMagicianWeb.Models.Ingredients;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Models.Cocktails
{
    public class UpdateCocktailViewModel
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public IFormFile NewPicture { get; set; }
        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        [Required, MinLength(2)]
        public List<IngredientViewModel> CurrentIngredients { get; set; } = new List<IngredientViewModel>(10);
        public List<string> Ingredients { get; set; } = new List<string>(20);
        public List<string> OfferingBars { get; set; } = new List<string>(20);
        public List<string> AllBars { get; set; } = new List<string>(20);
        [Required, MinLength(1)]
        public List<int> Quantities { get; set; } = new List<int>(10);
    }
}
