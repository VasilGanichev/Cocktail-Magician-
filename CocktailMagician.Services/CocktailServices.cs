using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class CocktailServices : ICocktailServices
    {
        private CocktailDB _context;

        public CocktailServices(CocktailDB context)
        {
            this._context = context;
        }

        public async Task<Cocktail> AddAsync(string name, byte[] image)
        {
            var cocktail = new Cocktail
            {
                Name = name,
                Picture = image,
            };

            await _context.Cocktails.AddAsync(cocktail);
            await _context.SaveChangesAsync();

            return cocktail;
        }
        public async Task<List<Cocktail>> GetMultipleCocktailsByNameAsync(string input)
        {
            var cocktails = await _context.Cocktails.Where(c => c.Name.Contains(input)).ToListAsync();
            return cocktails;
        }

        public async Task<Cocktail> GetByIdAsync(int id)
        {
            var cocktail = await _context.Cocktails.Include(c => c.Bars).Include(c => c.Ingredients).ThenInclude(i => i.Ingredient).FirstOrDefaultAsync(c => c.Id == id);

            return cocktail;
        }

        public async Task UpdateCocktail(Cocktail cocktail)
        {
            _context.Cocktails.Update(cocktail);
            await _context.SaveChangesAsync();
        }

        public async Task HideAsync(int id)
        {
            var cocktail = await GetByIdAsync(id);
            cocktail.IsHidden = true;
            await _context.SaveChangesAsync();
        }

        public async Task UnhideAsync(int id)
        {
            var cocktail = await GetByIdAsync(id);
            cocktail.IsHidden = false;
            await _context.SaveChangesAsync();
        }
    }
}
