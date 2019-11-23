using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.CocktailServicesTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnCorrectCocktailByID()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCocktailByID));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);

                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var result = sut.GetAsync(cocktail.Id).GetAwaiter().GetResult();

                Assert.AreEqual(cocktail, result);
            }
        }

        [TestMethod]
        public void ReturnCorrectCocktailByName()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCocktailByName));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);

                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var result = sut.GetAsync(cocktail.Name).GetAwaiter().GetResult();

                Assert.AreEqual(cocktail, result);
            }
        }
    }
}
