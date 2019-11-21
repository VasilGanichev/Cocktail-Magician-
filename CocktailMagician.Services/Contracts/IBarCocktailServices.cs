using CocktailMagician.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IBarCocktailServices
    {
        Task<BarCocktail>CreateAsync(Bar bar, Cocktail cocktail);
        Task DeleteAsync(Bar bar, Cocktail cocktail);
    }
}
