using CocktailMagician.Data.Entities;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IReviewServices
    {
        Task<Review> GetReviewAsync(int id);
        Task<Review> CreateReviewAsync(double rating, string comment, Bar bar, Cocktail cocktail, User user);
        Task DeleteAsync(int id);
    }
}
