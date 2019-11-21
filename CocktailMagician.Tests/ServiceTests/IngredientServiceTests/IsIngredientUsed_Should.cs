using CocktailMagician.Data;
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
    }
}
