using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.CocktailIngredientServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void DeleteTheDesiredCocktailIngredient()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(DeleteTheDesiredCocktailIngredient));

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
                Assert.IsTrue(assertContext.CocktailIngredients.Contains(barCocktail));

                sut.DeleteAsync(cocktail.Name, ingredient.Name).GetAwaiter().GetResult();
                Assert.IsFalse(assertContext.CocktailIngredients.Contains(barCocktail));
            }
        }
    }
}
