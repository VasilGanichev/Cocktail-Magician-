using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailMagician.Tests.ServiceTests.CocktailReviewServicesTest
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void CorrectlyDeleteReview()
        {
            var options = TestUtilities.GetOptions(nameof(CorrectlyDeleteReview));
            int resultId = -1;
            var review1 = new CocktailReview
            {
                Id = resultId,
                CocktailId = 1,
            };
            var review2 = new CocktailReview
            {
                Id = -2,
                CocktailId = 1,
            };
            var review3 = new CocktailReview
            {
                Id = -3,
                CocktailId = 2,
            };
            using (var actContext = new CocktailDB(options))
            {
                actContext.AddAsync(review1).GetAwaiter();
                actContext.AddAsync(review2).GetAwaiter();
                actContext.AddAsync(review3).GetAwaiter();
                actContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailReviewServices(assertContext);
                sut.DeleteAsync(resultId).GetAwaiter();
                Assert.IsFalse(assertContext.CocktailReviews.Contains(review1));
            }
        }
        [TestMethod]
        public void ThrowWhenIdIsNotFound()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowWhenIdIsNotFound));
            int resultId = -1;
            var review1 = new CocktailReview
            {
                Id = resultId,
                CocktailId = 1,
            };
            var review2 = new CocktailReview
            {
                Id = -2,
                CocktailId = 1,
            };
            var review3 = new CocktailReview
            {
                Id = -3,
                CocktailId = 2,
            };
            using (var actContext = new CocktailDB(options))
            {
                actContext.AddAsync(review1).GetAwaiter();
                actContext.AddAsync(review2).GetAwaiter();
                actContext.AddAsync(review3).GetAwaiter();
                actContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(
                    () => sut.DeleteAsync(int.MaxValue));
            }
        }
    }
}
