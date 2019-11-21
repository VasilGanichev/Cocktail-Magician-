
using System.Collections.Generic;
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

        public async Task DeleteAsync(string cocktailName, string ingredientName)
        {
            var cocktailIngredient = await _context.CocktailIngredients.FirstOrDefaultAsync(c => c.Cocktail.Name == cocktailName && c.Ingredient.Name == ingredientName);
            _context.CocktailIngredients.Remove(cocktailIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ingredient ingredient, Cocktail cocktail, int quantity)
        {
            var cocktailIngredient = await _context.CocktailIngredients.FirstOrDefaultAsync(c => c.Cocktail.Name == cocktail.Name && c.Ingredient.Name == ingredient.Name);
            cocktailIngredient.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PairExistsAsync(Ingredient ingredient, Cocktail cocktail)
        {
            var boolCheck = await _context.CocktailIngredients.Include(c => c.Ingredient).Include(c => c.Cocktail).AnyAsync(c => c.Ingredient.Name == ingredient.Name && c.Cocktail == cocktail);

            return boolCheck;
        }

        public async Task<bool> IsPairUpdatedAsync(Ingredient ingredient, Cocktail cocktail, int quantity)
        {
            var boolCheck = await _context.CocktailIngredients.Include(c => c.Ingredient).Include(c => c.Cocktail).AnyAsync(c => c.Ingredient.Name == ingredient.Name && c.Cocktail == cocktail && c.Quantity == quantity);

            return !boolCheck;
        }
    }
}
