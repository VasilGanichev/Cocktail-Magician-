using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.CocktailServicesTests
{
    [TestClass]
    public class GetCollection_Should
    {
        [TestMethod]
        public void ReturnCollectionWithCocktails()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionWithCocktails));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var result = sut.GetCollectionAsync().GetAwaiter().GetResult().ToArray();

                Assert.IsTrue(result.Length == 2 && result[0] == cocktail && result[1]== cocktail2);
            }
        }
    }
}
