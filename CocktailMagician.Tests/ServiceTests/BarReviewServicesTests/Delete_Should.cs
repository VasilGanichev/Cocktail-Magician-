using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CocktailMagician.Tests.ServiceTests.BarReviewServicesTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void CorrectlyDeleteReview()
        {
            var options = TestUtilities.GetOptions(nameof(CorrectlyDeleteReview));
            int resultId = -1;
            var review1 = new BarReview
            {
                Id = resultId,
                BarId = 1,
            };
            var review2 = new BarReview
            {
                Id = -2,
                BarId = 1,
            };
            var review3 = new BarReview
            {
                Id = -3,
                BarId = 2,
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
                var sut = new BarReviewServices(assertContext);
                sut.DeleteAsync(resultId).GetAwaiter();
                Assert.IsFalse(assertContext.BarReviews.Contains(review1));
            }
        }
        [TestMethod]
        public void ThrowWhenIdIsNotFound()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowWhenIdIsNotFound));
            int resultId = -1;
            var review1 = new BarReview
            {
                Id = resultId,
                BarId = 1,
            };
            var review2 = new BarReview
            {
                Id = -2,
                BarId = 1,
            };
            var review3 = new BarReview
            {
                Id = -3,
                BarId = 2,
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
                var sut = new BarReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(
                    () => sut.DeleteAsync(int.MaxValue));
            }
        }
    }
}
