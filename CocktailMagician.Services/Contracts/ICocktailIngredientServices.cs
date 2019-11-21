
using CocktailMagician.Data.Entities;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailIngredientServices
    {
        Task<CocktailIngredient> AddAsync(Cocktail cocktail, Ingredient ingredient, int quantity);
        Task<bool> PairExistsAsync(Ingredient ingredient, Cocktail cocktail);
        Task DeleteAsync(string cocktailName, string ingredientName);
        Task<bool> IsPairUpdatedAsync(Ingredient ingredient, Cocktail cocktail, int quantity);
        Task UpdateAsync(Ingredient ingredient, Cocktail cocktail, int quantity);
    }
}
