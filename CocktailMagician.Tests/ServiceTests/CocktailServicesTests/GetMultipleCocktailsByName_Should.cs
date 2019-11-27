using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.CocktailServicesTests
{
    [TestClass]
    public class GetMultipleCocktailsByName_Should
    {
        [TestMethod]
        public void ReturnCollectionOfCorrectCocktails()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfCorrectCocktails));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();

                var result = sut.GetMultipleCocktailsByNameAsync("u").GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 2 && result.Contains(cocktail) && result.Contains(cocktail2));
            }
        }

        [TestMethod]
        public void ReturnEmptyCollectionIfNoCocktailsAreFound()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionIfNoCocktailsAreFound));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var result = sut.GetMultipleCocktailsByNameAsync("u").GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }
    }
}
