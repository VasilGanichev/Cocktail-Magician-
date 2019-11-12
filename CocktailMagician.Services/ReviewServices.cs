using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class ReviewServices : IReviewServices
    {
        public Task<Review> CreateReviewAsync(double rating, string comment, Bar bar, Cocktail cocktail, User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
