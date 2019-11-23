using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class CocktailReviewServices : ICocktailReviewServices
    {
        private readonly CocktailDB context;

        public CocktailReviewServices(CocktailDB context)
        {
            this.context = context;
        }

        public async Task<CocktailReview> CreateCocktailReviewAsync(double rating, string comment, Cocktail cocktail, User user)
        {
            user.EnsureNotNull();
            cocktail.EnsureNotNull();

            var review = new CocktailReview
            {
                Rating = rating,
                Comment = comment,
                UserName = user.UserName,
                Cocktail = cocktail,
                CocktailId = cocktail.Id,
                User = user,
                UserID = user.Id,
            };
            await this.context.CocktailReviews.AddAsync(review);
            await this.context.SaveChangesAsync();
            return review;
        }
        public async Task<CocktailReview> GetCocktailReviewAsync(int id)
        {
            var review = await this.context.CocktailReviews.FirstOrDefaultAsync(r => r.Id == id);
            return review;
        }

        public async Task DeleteAsync(int id)
        {
            var review = await GetCocktailReviewAsync(id);
            review.EnsureNotNull();
            this.context.CocktailReviews.Remove(review);
            await this.context.SaveChangesAsync();    
        }


        public async Task<List<CocktailReview>> GetCocktailReviewsCollectionAsync(int cocktailId)
        {
            var reviews = await (this.context.CocktailReviews.Where(r => r.CocktailId == cocktailId)).ToListAsync();
            return reviews;
        }
    }
}
