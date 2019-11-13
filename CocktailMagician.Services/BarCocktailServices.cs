using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;

namespace CocktailMagician.Services
{
    public class BarCocktailServices : IBarCocktailServices
    {
        private readonly CocktailDB _context;

        public BarCocktailServices(CocktailDB context)
        {
            _context = context;
        }

        public async Task<BarCocktail> CreateAsync(Bar bar, Cocktail cocktail)
        {
            var barCocktail = new BarCocktail
            {
                Bar = bar,
                BarID = bar.Id,
                Cocktail = cocktail,
                CocktailID = cocktail.Id
            };

            await _context.BarCocktails.AddAsync(barCocktail);
            await _context.SaveChangesAsync();

            return barCocktail;
        }
    }
}
