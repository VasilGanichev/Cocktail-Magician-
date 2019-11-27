using CocktailMagician.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace CocktailMagician.Services.Contracts
{
    public interface IBarServices
    {
        Task<IReadOnlyCollection<Bar>> GetCollectionAsync();
        Task CreateBarAsync(Bar bar);
        Task CreateBarAsync(string name, string adress, string phoneNumber, byte[] picture);
        Task<IReadOnlyCollection<Bar>> SearchBarsByMultipleCriteriaAsync(string name, string adress, string phonenumber, bool displayOnlyHiddenFiles);
        Task<Bar> GetBarAsync(int id);
        Task EditBarAsync(Bar bar);
        Task<Bar> GetAsync(string barName);
        Task<List<string>> LoadMoreCocktails(int alreadyLoaded, int barId);
        Task<List<Bar>> LoadNewestBars();
        Task<List<Bar>> GetMultipleBarsByNameAsync(string input);
        Task<bool> BarWithThatNameExists(string name);
    }
}
