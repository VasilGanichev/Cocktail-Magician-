using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Tests.ServiceTests.ReviewsServiceTests
{
    [TestClass]
    public class CreateBarReview_Sould
    {
        [TestMethod]
        public void ThrowWhenBarIsNull()
        {

            var options = TestUtilities.GetOptions(nameof(ThrowWhenBarIsNull));
            var user = new User
            {
                Id = "asd",
            };
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                sut.CreateBarReviewAsync(2, "134", null, user));
            }
        }
        [TestMethod]
        public void ThrowWhenUserIsNull()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowWhenUserIsNull));
            var bar = new Bar
            {
                Id = 1,
            };

            var user = new User
            {
                Id = "asd",
            };
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                sut.CreateBarReviewAsync(2, "asd", bar, user));
            }
        }
       
        [TestMethod]
        public void CorrectlyAddBarReview()
        {
            var options = TestUtilities.GetOptions(nameof(CorrectlyAddBarReview));
            var bar = new Bar
            {
                Id = 1,
            };
            var user = new User
            {
                Id = "asd",
            };
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarReviewServices(assertContext);
                var review = sut.CreateBarReviewAsync(5, "1adf", bar, user).GetAwaiter().GetResult();
                var result = assertContext.BarReviews.Find(review.Id);
                Assert.AreEqual(review, result);
            }

        }
       
    }
}
