using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    public class GetIngedientsByType_Should
    {
        [TestMethod]
        public void ShouldReturnCollectionOfCorrectIngredients()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ShouldReturnCollectionOfCorrectIngredients));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                name = "Rum2";
                var ingredient2 = sut.AddAsync(name, type).GetAwaiter().GetResult();
                var result = sut.GetIngedientsByTypeAsync("alcohol").GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 2 && result.Contains(ingredient) && result.Contains(ingredient));
            }
        }

        [TestMethod]
        public void ShouldReturnEmptyCollectionIfNoIngredientsAreFound()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ShouldReturnEmptyCollectionIfNoIngredientsAreFound));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var result = sut.GetIngedientsByTypeAsync("alcohol").GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }
    }
}
