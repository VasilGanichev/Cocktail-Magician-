using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.IngredientServiceTests
{
    [TestClass]
    class IngredientWithThatNameExists_Should
    {
        [TestMethod]
        public void ReturnTureIfSuchIngredientExists()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTureIfSuchIngredientExists));
            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "test",
            };

            //act & assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                assertContext.Ingredients.Add(ingredient);
                assertContext.SaveChanges();

                Assert.IsTrue(sut.IngredientWithThatNameExists(ingredient.Name).GetAwaiter().GetResult());
            }
        }

        [TestMethod]
        public void ReturnFalseIfSuchIngredientDoesntExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalseIfSuchIngredientDoesntExist));

            //act & assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new IngredientServices(assertContext);
                Assert.IsFalse(sut.IngredientWithThatNameExists("test").GetAwaiter().GetResult());
            }
        }
    }
}
