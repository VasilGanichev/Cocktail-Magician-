using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Tests.ServiceTests.CocktailServicesTests
{
    [TestClass]
    public class Hide_Should
    {
        [TestMethod]
        public void HideTheDesiredCocktail()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(HideTheDesiredCocktail));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                sut.HideAsync(cocktail.Id).GetAwaiter().GetResult();


                Assert.IsTrue(cocktail.IsHidden == true);
            }
        }
    }
}
