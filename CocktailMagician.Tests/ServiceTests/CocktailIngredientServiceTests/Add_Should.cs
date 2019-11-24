using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.CocktailIngredientServiceTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public void CreateNewInstanceOfCocktailIngredient()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(CreateNewInstanceOfCocktailIngredient));

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
                var cocktailIngredient = sut.AddAsync(cocktail, ingredient, quantity).GetAwaiter().GetResult();

                Assert.IsInstanceOfType(cocktailIngredient, typeof(CocktailIngredient));
            }
        }

        [TestMethod]
        public void AddTheCocktailIngredientToTheDBSetCorrectly()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(AddTheCocktailIngredientToTheDBSetCorrectly));

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
                var cocktailIngredient = sut.AddAsync(cocktail, ingredient, quantity).GetAwaiter().GetResult();

                Assert.IsTrue(assertContext.CocktailIngredients.Contains(cocktailIngredient));
            }
        }
    }
}
