using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Services.Utilities;
using CocktailMagician.Data;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly CocktailDB context;

        public ReviewServices(CocktailDB context)
        {
            this.context = context;
        }

        public async Task<Review> CreateReviewAsync(double rating, string comment, Bar bar, Cocktail cocktail, User user)
        {
            user.EnsureNotNull();
            if (bar != null && cocktail != null)
                throw new ArgumentException();
            if (bar == null)
            {
                cocktail.EnsureNotNull();
                var review = new Review
                {
                    Rating = rating,
                    Comment = comment,
                    Cocktail = cocktail,
                    CocktailID = cocktail.ID,
                    User = user,
                    UserID = user.Id,
                };
                await this.context.Reviews.AddAsync(review);
                await this.context.SaveChangesAsync();
                return review;
            }
            else
            {
                var review = new Review
                {
                    Rating = rating,
                    Comment = comment,
                    Bar = bar,
                    BarId = bar.Id,
                    User = user,
                    UserID = user.Id,
                };
                await this.context.Reviews.AddAsync(review);
                await this.context.SaveChangesAsync();
                return review;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var review = await GetReviewAsync(id);
            review.EnsureNotNull();
            this.context.Remove(review);
            await this.context.SaveChangesAsync();
        }

        public async Task<Review> GetReviewAsync(int id)
        {
            var review = await this.context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            review.EnsureNotNull();
            return review;
        }
        public async Task EditReview(Review review, double newRating, string newComment)
        {
            throw new NotImplementedException();
        }
    }
}
