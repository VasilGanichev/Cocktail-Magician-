using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.CocktailIngredientServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void UpdateTheQuantityOfCocktailIngredient()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(UpdateTheQuantityOfCocktailIngredient));

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
                var barCocktail = sut.AddAsync(cocktail, ingredient, quantity).GetAwaiter().GetResult();
                quantity = 2;
                sut.UpdateAsync(ingredient, cocktail, quantity).GetAwaiter().GetResult();

                Assert.IsTrue(barCocktail.Quantity == 2);
            }
        }
    }
}
