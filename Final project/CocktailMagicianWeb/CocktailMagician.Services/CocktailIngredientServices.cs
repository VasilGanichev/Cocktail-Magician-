using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;

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
                CocktailID = cocktail.ID,
                Cocktail = cocktail,
                IngredientID = ingredient.ID,
                Ingredient = ingredient,
                Quantity = quantity
            };

            await _context.CocktailIngredients.AddAsync(cocktailIngredient);
            await _context.SaveChangesAsync();

            return cocktailIngredient;
        }
    }
}
