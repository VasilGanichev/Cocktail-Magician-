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
    public class GetBarCollection_Should
    {
        [TestMethod]
        public void ReturnCorrectReviews()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectReviews));
            int resultId = 1;
            var review1 = new BarReview
            {
                Id = -1,
                BarId =1,
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
                var result = sut.GetBarReviewsCollectionAsync(resultId).GetAwaiter().GetResult();
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count);
                Assert.IsTrue(result.Select(br => br.BarId).Contains(1));
            }
        }
        [TestMethod]
        public void ReturnNullWhenBarIdIsNotFound()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnNullWhenBarIdIsNotFound));
            var review1 = new BarReview
            {
                Id = -1,
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
                var result = sut.GetBarReviewsCollectionAsync(int.MaxValue).GetAwaiter().GetResult();
                Assert.AreEqual(0,result.Count);

            }
        }
    }
}
