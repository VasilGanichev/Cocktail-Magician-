using System.Collections.Generic;

namespace CocktailMagicianWeb.Models.Cocktails
{
    public class CocktailsViewModel
    {
        public string Input { get; set; }
        public List<CocktailViewModel> Cocktails { get; set; } = new List<CocktailViewModel>(); 
    }
}
