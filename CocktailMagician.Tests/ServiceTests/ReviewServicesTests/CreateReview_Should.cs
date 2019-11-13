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
    public class CreateReview_Sould
    {
        [TestMethod]
        public void ThrowWhenBarAndCocktailsAreNull()
        {

            var options = TestUtilities.GetOptions(nameof(ThrowWhenBarAndCocktailsAreNull));
            var user = new User
            {
                Id = "asd",
            };
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new ReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                sut.CreateReviewAsync(2, "134", null, null, user));
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
            var cocktail = new Cocktail
            {
                Id = 1,
            };
            var user = new User
            {
                Id = "asd",
            };
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new ReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                sut.CreateReviewAsync(2, "asd", bar, cocktail, user));
            }
        }
        [TestMethod]
        public void ThrowWhenBothCoctailAndBarAreNotNull()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowWhenBothCoctailAndBarAreNotNull));

            var bar = new Bar
            {
                Id = 1,
            };

            using (var assertContext = new CocktailDB(options))
            {
                var sut = new ReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                sut.CreateReviewAsync(2, "asd", bar, null, null));
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
                var sut = new ReviewServices(assertContext);
                var review = sut.CreateReviewAsync(5, "1adf", bar, null, user).GetAwaiter().GetResult();
                var result = assertContext.Reviews.Find(review.Id);
                Assert.AreEqual(review, result);
                Assert.IsNull(result.Cocktail);
            }

        }
        [TestMethod]
        public void CorrectlyAddCocktailReview()
        {
            var options = TestUtilities.GetOptions(nameof(CorrectlyAddCocktailReview));
            var cocktail = new Cocktail
            {
                Id = 1,
            };
            var user = new User
            {
                Id = "asd",
            };
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new ReviewServices(assertContext);
                var review = sut.CreateReviewAsync(5, "1adf", null, cocktail, user).GetAwaiter().GetResult();
                var result = assertContext.Reviews.Find(review.Id);
                Assert.AreEqual(review, result);
                Assert.IsNull(result.Bar);
            }
        }
    }
}
