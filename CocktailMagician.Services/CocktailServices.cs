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

        public async Task<Cocktail> GetAsync(int id)
        {
            var cocktail = await _context.Cocktails
                .Include(c => c.CocktailReviews)
                .Include(c => c.Bars)
                .ThenInclude(c => c.Bar)
                .Include(c => c.Ingredients)
                .ThenInclude(i => i.Ingredient).FirstOrDefaultAsync(c => c.Id == id);

            return cocktail;
        }

        public async Task<Cocktail> GetAsync(string name)
        {
            var cocktail = await _context.Cocktails.Include(c => c.Bars).ThenInclude(b => b.Bar).FirstOrDefaultAsync(c => c.Name == name);

            return cocktail;
        }

        public async Task UpdateCocktailAsync(Cocktail cocktail)
        {
            var dbCocktail = await GetAsync(cocktail.Id);
            dbCocktail.Name = cocktail.Name;
            if (cocktail.Picture != null)
            {
                dbCocktail.Picture = cocktail.Picture;
            }

            await _context.SaveChangesAsync();
        }

        public async Task HideAsync(int id)
        {
            var cocktail = await GetAsync(id);
            cocktail.IsHidden = true;
            await _context.SaveChangesAsync();
        }

        public async Task UnhideAsync(int id)
        {
            var cocktail = await GetAsync(id);
            cocktail.IsHidden = false;
            await _context.SaveChangesAsync();
        }
        public async Task<List<Cocktail>> SearchByMultipleCriteriaAsync(string name, string ingredientName, bool IncludeOnlyAlcohol)
        {
            List<Cocktail> cocktailsResult = new List<Cocktail>();
            if (IncludeOnlyAlcohol)
            {
                cocktailsResult = await _context.Cocktails
                  .Include(r => r.CocktailReviews)
                    .Include(b => b.Ingredients)
                    .ThenInclude(b => b.Ingredient)
                    .Where(b => ((name == null) || (b.Name.Contains(name))) &&
                    ((ingredientName == null) || (b.Ingredients.FirstOrDefault(i => i.Ingredient.Name == ingredientName) != null)) &&
                   (((b.Ingredients.Select(i => i.Ingredient.Type)).Contains("alcohol"))))
                    .ToListAsync();
            }
            else
            {
                cocktailsResult = await _context.Cocktails
                .Include(r => r.CocktailReviews)
                  .Include(b => b.Ingredients)
                  .ThenInclude(b => b.Ingredient)
                  .Where(b => ((name == null) || (b.Name.Contains(name))) &&
                  ((ingredientName == null) || (b.Ingredients.Select(i => i.Ingredient.Name).Contains(ingredientName)))).ToListAsync();
            }

            return cocktailsResult;
        }

        public async Task<IReadOnlyCollection<Cocktail>> GetCollectionAsync()
        {
            return await _context.Cocktails.ToListAsync();
        }
        public async Task<List<Cocktail>> LoadNewestCocktails()
        {
            var cocktails = await _context.Cocktails.Include(c => c.CocktailReviews).Where(c => c.IsHidden == false).OrderBy(c => c.CreatedOn).Take(5).ToListAsync();
            return cocktails;
        }

        public async Task<bool> CocktailWithThatNameExists(string name)
        {
            var boolCheck = await _context.Cocktails.AnyAsync(c => c.Name == name);

            return boolCheck;
        }
        public async Task<List<string>> LoadMoreBars(int alreadyLoaded, int cocktailId)
        {
            var cocktail = await GetAsync(cocktailId);
            var bars = cocktail.Bars.Select(bc => bc.Bar.Name).Skip(alreadyLoaded).Take(10).ToList();
            return bars;
        }
    }
}
