using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void DeleteTheDesiredIngredient()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(DeleteTheDesiredIngredient));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                var name = "Rum";
                var type = "alcohol";
                var ingredient = sut.AddAsync(name, type).GetAwaiter().GetResult();
                sut.DeleteAsync(ingredient.ID).GetAwaiter().GetResult();

                Assert.IsNull(assertContext.Ingredients.Find(ingredient.ID));
            }
        }
    }
}
