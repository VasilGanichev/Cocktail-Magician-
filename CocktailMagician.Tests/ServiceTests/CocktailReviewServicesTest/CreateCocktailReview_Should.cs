using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Tests.ServiceTests.CocktailReviewServicesTest
{
    [TestClass]
    public class CreateCocktailReview_Should
    {
        [TestMethod]
        public void ThrowWhenCocktailIsNull()
        {

            var options = TestUtilities.GetOptions(nameof(ThrowWhenCocktailIsNull));
            var user = new User
            {
                Id = "asd",
            };
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                sut.CreateCocktailReviewAsync(2, "134", null, user));
            }
        }
        [TestMethod]
        public void ThrowWhenUserIsNull()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowWhenUserIsNull));
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
                var sut = new CocktailReviewServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                sut.CreateCocktailReviewAsync(2, "asd", cocktail, user));
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
                var sut = new CocktailReviewServices(assertContext);
                var review = sut.CreateCocktailReviewAsync(5, "1addsf", cocktail, user).GetAwaiter().GetResult();
                var result = assertContext.CocktailReviews.Find(review.Id);
                Assert.AreEqual(review, result);
            }

        }


    }
}
