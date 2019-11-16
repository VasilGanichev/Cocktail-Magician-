using CocktailMagicianWeb.Models.Ingredients;
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
        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        [Required, MinLength(2)]
        public List<IngredientViewModel> Ingredients { get; set; } = new List<IngredientViewModel>(10);
        public List<BarViewModel> Bars { get; set; } = new List<BarViewModel>(20);
        [Required, MinLength(1)]
        public List<int> Quantities { get; set; } = new List<int>(10);
    }
}
