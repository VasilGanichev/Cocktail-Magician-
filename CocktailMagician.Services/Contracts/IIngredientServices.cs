using CocktailMagician.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IIngredientServices
    {
        Task<Ingredient> AddAsync(string name, string type);
        Task<Ingredient> GetAsync(int id);
        Task UpdateAsync(int id, string name);
        Task DeleteAsync(int id);
        Task<List<Ingredient>> GetMultipleIngredientsByNameAsync(string input);
        Task<bool> IsIngredientUsedAsync(int id);
    }
}
