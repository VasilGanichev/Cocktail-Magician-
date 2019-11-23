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
    public class GetBarReview_Should
    {
        [TestMethod]
        public void ReturnCorrectReview()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectReview));
            int resultId = -1;
            var review = new BarReview
            {
                Id = resultId,
            };
            using (var actContext = new CocktailDB(options))
            {
                actContext.AddAsync(review).GetAwaiter();
                actContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarReviewServices(assertContext);
                var result = sut.GetBarReviewAsync(resultId).GetAwaiter().GetResult();
                Assert.IsNotNull(result);
                Assert.AreEqual(resultId, result.Id);
            }
        }
        [TestMethod]
        public void ReturnNullWhenIdNotFound()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnNullWhenIdNotFound));
            int resultId = -1;
            var review = new BarReview
            {
                Id = resultId,
            };
            using (var actContext = new CocktailDB(options))
            {
                actContext.AddAsync(review).GetAwaiter();
                actContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarReviewServices(assertContext);
                var result = sut.GetBarReviewAsync(int.MaxValue).GetAwaiter().GetResult();
                Assert.IsNull(result);
            }
        }
    }
}
