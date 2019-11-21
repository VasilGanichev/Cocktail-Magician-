
using CocktailMagician.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailServices
    {
        Task<Cocktail> AddAsync(string name, byte[] image);
        Task<Cocktail> FindCocktailByNameAsync(string name);
        Task<List<Cocktail>> GetMultipleCocktailsByNameAsync(string input);
        Task<List<Cocktail>> SearchByMultipleCriteriaAsync(string name, string ingredientName, bool IncludeAlcohol);
        Task<Cocktail> GetByIdAsync(int id);
        Task<IReadOnlyCollection<Cocktail>> GetCollectionAsync();
        Task HideAsync(int id);
        Task UnhideAsync(int id);
    }
}
