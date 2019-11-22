using CocktailMagician.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailReviewServices
    {
        Task<CocktailReview> GetCocktailReviewAsync(int id);
        Task<CocktailReview> CreateCocktailReviewAsync(double rating, string comment, Cocktail cocktail, User user);
        Task DeleteAsync(int id);
        Task EditCocktailReview(CocktailReview review, double newRating, string newComment);
        Task<List<CocktailReview>> GetCocktailReviewsCollectionAsync(int cocktailId);
    }
}
