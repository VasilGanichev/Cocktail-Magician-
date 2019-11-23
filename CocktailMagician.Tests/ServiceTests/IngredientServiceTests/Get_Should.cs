using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnCorrectIngredientByID()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectIngredientByID));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);

                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                var result = sut.GetAsync(ingredient.Id).GetAwaiter().GetResult();

                Assert.AreEqual(ingredient, result);
            }
        }

        public void ReturnCorrectIngredientByName()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectIngredientByName));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);

                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                var result = sut.GetAsync(ingredient.Name).GetAwaiter().GetResult();

                Assert.AreEqual(ingredient, result);
            }
        }
    }
}
