using CocktailMagician.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IIngredientServices
    {
        Task<Ingredient> AddAsync(string name, string type);
        Task<List<Ingredient>> GetMultipleIngredientsByNameAsync(List<string> names);
        Task<Ingredient> GetAsync(int id);
        Task<List<Ingredient>> GetIngedientsByTypeAsync(string type);
        Task UpdateAsync(int id, string name);
        Task DeleteAsync(int id);
        Task<List<Ingredient>> SearchIngredientsAsync(string input);
        Task<bool> IsIngredientUsedAsync(int id);
        Task<bool> IngredientWithThatNameExists(string name);
    }
}
