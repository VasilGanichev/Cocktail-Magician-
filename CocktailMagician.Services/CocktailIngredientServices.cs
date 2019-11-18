
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class CocktailIngredientServices : ICocktailIngredientServices
    {
        private readonly CocktailDB _context;

        public CocktailIngredientServices(CocktailDB context)
        {
            this._context = context;
        }
        public async Task<CocktailIngredient> AddAsync(Cocktail cocktail, Ingredient ingredient, int quantity)
        {
            var cocktailIngredient = new CocktailIngredient
            {
                CocktailID = cocktail.Id,
                Cocktail = cocktail,
                IngredientID = ingredient.Id,
                Ingredient = ingredient,
                Quantity = quantity
            };

            await _context.CocktailIngredients.AddAsync(cocktailIngredient);
            await _context.SaveChangesAsync();

            return cocktailIngredient;
        }

        public async Task<bool> PairExists(Ingredient ingredient, Cocktail cocktail)
        {
            var boolCheck = await _context.CocktailIngredients.Include(c => c.Ingredient).Include(c => c.Cocktail).AnyAsync(c => c.Ingredient == ingredient && c.Cocktail == cocktail);

            return boolCheck;
        }
    }
}
