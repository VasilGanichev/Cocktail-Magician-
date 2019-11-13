
using CocktailMagician.Data.Entities;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailIngredientServices
    {
        Task<CocktailIngredient> AddAsync(Cocktail cocktail, Ingredient ingredient, int quantity);
    }
}
