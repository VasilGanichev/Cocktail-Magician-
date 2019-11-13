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
            var cocktails = await _context.Cocktails.Where(i => i.Name.Contains(input)).ToListAsync();
            return cocktails;
        }

        public async Task<List<Bar>> GetCollectionAsync()
        {
            var bars = await _context.Bars.ToListAsync();
            return bars;
        }
    }
}
