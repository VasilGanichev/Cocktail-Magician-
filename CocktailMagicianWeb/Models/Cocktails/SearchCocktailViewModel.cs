using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Models.Cocktails
{
    public class SearchCocktailViewModel
    {
        public string Name { get; set; }
        public string Ingredient { get; set; }
        public bool IncludeOnlyAlcoholicDrinks { get; set; }
        public List<CocktailViewModel> SearchResults { get; set; }
       
    }
}
