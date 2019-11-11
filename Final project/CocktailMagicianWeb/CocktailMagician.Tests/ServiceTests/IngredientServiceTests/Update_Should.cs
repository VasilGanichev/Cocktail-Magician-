using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void UpdateTheIngredientNameCorrectly()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(UpdateTheIngredientNameCorrectly));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);

                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                var newName = "Test12";
                sut.UpdateAsync(ingredient.ID, newName).GetAwaiter().GetResult();

                Assert.AreEqual(newName, ingredient.Name);
            }
        }
    }
}
