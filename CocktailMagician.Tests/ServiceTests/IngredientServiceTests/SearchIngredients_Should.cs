using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    public class SearchIngredients_Should
    {
        [TestMethod]
        public void ReturnCollectionOfCorrectIngredientsContainingTheSpecifiedChar()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfCorrectIngredientsContainingTheSpecifiedChar));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                name = "Rum2";
                var ingredient2 = sut.AddAsync(name, type).GetAwaiter().GetResult();
                var result = sut.GetMultipleIngredientsByNameAsync("u").GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 2 && result.Contains(ingredient) && result.Contains(ingredient));
            }
        }
    }
}
