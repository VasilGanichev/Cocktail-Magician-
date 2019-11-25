using CocktailMagician.Data.Entities;
using CocktailMagicianWeb.Models.Cocktails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Models.Home
{
    public class HomeViewModel
    {
        public List<BarViewModel> Bars { get; set; }
        public List<CocktailViewModel> Cocktails { get; set; }
    }
}
