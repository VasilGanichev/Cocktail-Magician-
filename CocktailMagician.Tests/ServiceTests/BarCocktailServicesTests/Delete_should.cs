using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.BarCocktailServicesTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void DeleteTheDesiredBarCocktail()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(DeleteTheDesiredBarCocktail));

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

                sut.DeleteAsync(bar, cocktail).GetAwaiter().GetResult();
                Assert.IsFalse(assertContext.BarCocktail.Contains(barCocktail));
            }
        }
    }
}
