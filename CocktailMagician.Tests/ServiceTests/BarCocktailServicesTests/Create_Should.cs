using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.BarCocktailServicesTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void CreateNewInstanceOfBarCocktail()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(CreateNewInstanceOfBarCocktail));

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
                Assert.IsInstanceOfType(barCocktail, typeof(BarCocktail));
            }
        }

        [TestMethod]
        public void AddTheBarCocktailToTheDBSetCorrectly()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(AddTheBarCocktailToTheDBSetCorrectly));

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

                Assert.IsTrue(assertContext.BarCocktail.Contains(barCocktail));
            }
        }
    }
}
