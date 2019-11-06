using CocktailMagician.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IBarServices
    {
        Task<IReadOnlyCollection<Bar>> GetCollectionAsync();
        Task CreateBarAsync(Bar bar);
        Task CreateBarAsync(string name, string adress, string phoneNumber, byte[] picture);
    }
}
