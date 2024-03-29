﻿using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return review;
        }

        public async Task<List<BarReview>> GetBarReviewsCollectionAsync(int barId)
        {
            var reviews = await (this.context.BarReviews.Where(r => r.BarId == barId)).ToListAsync();
            return reviews;
        }
    }

}



