﻿using CocktailMagician.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailServices
    {
        Task<Cocktail> AddAsync(string name, byte[] image);
        Task<List<Bar>> GetCollectionAsync();
        Task<List<Cocktail>> GetMultipleCocktailsByNameAsync(string input);
    }
}