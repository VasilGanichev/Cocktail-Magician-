using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.CocktailServicesTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public void CreateNewInstanceOfCocktail()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(CreateNewInstanceOfCocktail));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                Assert.IsInstanceOfType(cocktail, typeof(Cocktail));
            }
        }

        [TestMethod]
        public void AddTheCocktailToTheDBSetCOrrectly()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(AddTheCocktailToTheDBSetCOrrectly));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();

                Assert.IsTrue(assertContext.Cocktails.Contains(cocktail));
            }
        }
    }
}
