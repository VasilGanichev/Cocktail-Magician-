using CocktailMagician.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IBarReviewServices
    {
        Task<BarReview> GetBarReviewAsync(int id);
        Task<BarReview> CreateBarReviewAsync(double rating, string comment, Bar bar, User user);
        Task DeleteAsync(int id);
        Task EditBarReview(BarReview review, double newRating, string newComment);
        Task<List<BarReview>> GetBarReviewsCollectionAsync(int barId);
    }
}
