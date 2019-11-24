using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.BarCocktailServicesTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnCorrectInstanceById()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstanceById));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarCocktailServices(assertContext);
                var cocktail = new Cocktail
                {
                    Name = "Mojito"
                };
                var bar = new Bar
                {
                    Name = "Mojito Bar"
                };

                var barCocktail = sut.CreateAsync(bar, cocktail).GetAwaiter().GetResult();
                var result = sut.GetAsync(barCocktail.Id).GetAwaiter().GetResult();

                Assert.AreEqual(barCocktail, result);
            }
        }
    }
}
