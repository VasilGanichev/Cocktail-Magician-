using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.CocktailIngredientServiceTests
{
    [TestClass]
    public class IsPairUpdated_Should
    {
        [TestMethod]
        public void ReturnTrueIfThePairIsUpdated()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnTrueIfThePairIsUpdated));

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
                cocktailIngredient.Quantity = 2;
                assertContext.SaveChanges();

                Assert.IsTrue(sut.IsPairUpdatedAsync(ingredient, cocktail, 1).GetAwaiter().GetResult());
            }
        }

        [TestMethod]
        public void ReturnFalseIfThePairIsNotUpdated()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnFalseIfThePairIsNotUpdated));

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

                Assert.IsFalse(sut.IsPairUpdatedAsync(ingredient, cocktail, 1).GetAwaiter().GetResult());
            }
        }
    }
}
