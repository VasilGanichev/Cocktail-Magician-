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
    public class BarReviewServices : IBarReviewServices
    {
        private readonly CocktailDB context;

        public BarReviewServices(CocktailDB context)
        {
            this.context = context;
        }

        public async Task<BarReview> CreateBarReviewAsync(double rating, string comment, Bar bar, User user)
        {
            user.EnsureNotNull();
            bar.EnsureNotNull();

            var review = new BarReview
            {
                Rating = rating,
                Comment = comment,
                UserName = user.UserName,
                Bar = bar,
                BarId = bar.Id,
                User = user,
                UserID = user.Id,
            };
            await this.context.BarReviews.AddAsync(review);
            await this.context.SaveChangesAsync();
            return review;
        }
        public async Task DeleteAsync(int id)
        {
            var review = await GetBarReviewAsync(id);
            review.EnsureNotNull();
            this.context.Remove(review);
            await this.context.SaveChangesAsync();
        }

        public async Task<BarReview> GetBarReviewAsync(int id)
        {
            var review = await this.context.BarReviews.FirstOrDefaultAsync(r => r.Id == id);
            review.EnsureNotNull();
            return review;
        }
        public async Task EditBarReview(BarReview review, double newRating, string newComment)
        {
            throw new NotImplementedException();
        }
    }

}



