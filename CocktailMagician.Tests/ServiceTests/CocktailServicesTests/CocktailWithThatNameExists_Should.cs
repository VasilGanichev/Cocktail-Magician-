using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.CocktailServicesTests
{
    [TestClass]
    public class CocktailWithThatNameExists_Should
    {
        [TestMethod]
        public void ReturnTureIfSuchCocktailExists()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTureIfSuchCocktailExists));
            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "test",
            };

            //act & assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                assertContext.Cocktails.Add(cocktail);
                assertContext.SaveChanges();
                Assert.IsTrue(sut.CocktailWithThatNameExists(cocktail.Name).GetAwaiter().GetResult());
            }
        }

        [TestMethod]
        public void ReturnFalseIfSuchCocktailDoesntExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalseIfSuchCocktailDoesntExist));

            //act & assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                Assert.IsFalse(sut.CocktailWithThatNameExists("test").GetAwaiter().GetResult());
            }
        }
    }
}
