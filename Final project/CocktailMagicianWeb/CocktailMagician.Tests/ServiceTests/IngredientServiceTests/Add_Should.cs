using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public void CreateNewInstanceOfIngredient()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(CreateNewInstanceOfIngredient));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                Assert.IsInstanceOfType(ingredient, typeof(Ingredient));
            }
        }

        [TestMethod]
        public void AddTheIngredientToTheDBSetCOrrectly()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(AddTheIngredientToTheDBSetCOrrectly));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);

                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();

                Assert.IsTrue(assertContext.Ingredients.Contains(ingredient));
            }
        }
    }
}
