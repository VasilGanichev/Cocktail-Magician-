using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    public class GetMultipleIngredientsByName_Should
    {
        [TestMethod]
        public void ShouldReturnCollectionOfCorrectIngredientsByName()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ShouldReturnCollectionOfCorrectIngredientsByName));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                name = "Rum2";
                var ingredient2 = sut.AddAsync(name, type).GetAwaiter().GetResult();

                var names = new List<string> { "Rum", "Rum2" };
                var result = sut.GetMultipleIngredientsByNameAsync(names).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 2 && result.Contains(ingredient) && result.Contains(ingredient));
            }
        }

        [TestMethod]
        public void ReturnCollectionOfNullElementsIfThereAreNoMatches()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfNullElementsIfThereAreNoMatches));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var names = new List<string> { "Rum", "Rum2" };
                var result = sut.GetMultipleIngredientsByNameAsync(names).GetAwaiter().GetResult();

                Assert.IsTrue(result.All(e => e == null));
            }
        }
    }
}
