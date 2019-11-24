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
    public class GetCocktailCollection_Should
    {
        [TestMethod]
        public void ReturnCorrectReviews()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectReviews));
            int resultId = 1;
            var review1 = new CocktailReview
            {
                Id = -1,
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
                var result = sut.GetCocktailReviewsCollectionAsync(resultId).GetAwaiter().GetResult();
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count);
                Assert.IsTrue(result.Select(br => br.CocktailId).Contains(1));
            }
        }
        [TestMethod]
        public void ReturnNullWhenCocktailIdIsNotFound()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnNullWhenCocktailIdIsNotFound));
            var review1 = new CocktailReview
            {
                Id = -1,
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
                var result = sut.GetCocktailReviewsCollectionAsync(int.MaxValue).GetAwaiter().GetResult();
                Assert.AreEqual(0, result.Count);

            }
        }
    }
}
