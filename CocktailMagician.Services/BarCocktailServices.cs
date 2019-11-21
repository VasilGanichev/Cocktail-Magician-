using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

            await _context.BarCocktail.AddAsync(barCocktail);
            await _context.SaveChangesAsync();

            return barCocktail;
        }

        public async Task DeleteAsync(Bar bar, Cocktail cocktail)
        {
            var barCocktail = await _context.BarCocktail.FirstOrDefaultAsync(b => b.Bar == bar && b.Cocktail == cocktail);
            _context.BarCocktail.Remove(barCocktail);
            await _context.SaveChangesAsync();
        }

        private async Task<BarCocktail> GetAsync(int id)
        {
            var barCocktail = await _context.BarCocktail.FirstOrDefaultAsync(b => b.Id == id);

            return barCocktail;
        }
    }
}
