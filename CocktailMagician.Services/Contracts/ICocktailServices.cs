
using CocktailMagician.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailServices
    {
        Task<Cocktail> AddAsync(string name, byte[] image);
        Task<List<Cocktail>> GetMultipleCocktailsByNameAsync(string input);
        Task<Cocktail> GetAsync(int id);
        Task UpdateCocktailAsync(Cocktail cocktail);
        Task HideAsync(int id);
        Task UnhideAsync(int id);
        Task<List<Cocktail>> SearchByMultipleCriteriaAsync(string name, string ingredientName, bool IncludeOnlyAlcohol);
        Task<byte[]> GetCocktailCurrentPicture(int id);
        Task<Cocktail> GetAsync(string name);
        Task<IReadOnlyCollection<Cocktail>> GetCollectionAsync();
        Task<List<Cocktail>> LoadNewestCocktails();
        Task<bool> CocktailWithThatNameExists(string name);
        Task<List<string>> LoadMoreBars(int alreadyLoaded, int cocktailId);
    }
}
