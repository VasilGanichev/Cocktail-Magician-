using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    public class IsIngredientUsed_Should
    {
        [TestMethod]
        public void ReturnFalseIfTheIngredientIsNotUsedInAnyCocktail()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnFalseIfTheIngredientIsNotUsedInAnyCocktail));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                var result = sut.IsIngredientUsedAsync(ingredient.Id).GetAwaiter().GetResult();

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void ReturnTrueIfTheIngredientIsUsedInAnyCocktail()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnTrueIfTheIngredientIsUsedInAnyCocktail));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();

                var cocktail = new Cocktail
                {
                    Name = "Test",
                };

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };

                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();
                var result = sut.IsIngredientUsedAsync(ingredient.Id).GetAwaiter().GetResult();

                Assert.IsTrue(result);
            }
        }
    }
}
