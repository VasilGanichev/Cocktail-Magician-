using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.CocktailIngredientServiceTests
{
    [TestClass]
    public class PairExists_Should
    {
        [TestMethod]
        public void ReturnTrueIfThePairExists()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnTrueIfThePairExists));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailIngredientServices(assertContext);
                var cocktail = new Cocktail
                {
                    Name = "Mojito"
                };
                var ingredient = new Ingredient
                {
                    Name = "Mint"
                };
                var quantity = 1;
                var cocktailIngredient = sut.AddAsync(cocktail, ingredient, quantity).GetAwaiter().GetResult();

                Assert.IsTrue(sut.PairExistsAsync(ingredient, cocktail).GetAwaiter().GetResult());
            }
        }

        [TestMethod]
        public void ReturnFalseIfThePairDoesntExist()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnFalseIfThePairDoesntExist));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailIngredientServices(assertContext);
                var cocktail = new Cocktail
                {
                    Name = "Mojito"
                };
                var ingredient = new Ingredient
                {
                    Name = "Mint"
                };

                Assert.IsFalse(sut.PairExistsAsync(ingredient, cocktail).GetAwaiter().GetResult());
            }
        }
    }
}
