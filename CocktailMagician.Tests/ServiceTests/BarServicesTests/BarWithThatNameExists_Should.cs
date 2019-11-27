using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.BarServicesTests
{
    [TestClass]
    public class BarWithThatNameExists_Should
    {
        [TestMethod]
        public void ReturnTureIfSuchBarExists()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTureIfSuchBarExists));
            var bar = new Bar
            {
                Id = 1,
                Name = "test",
                Address = "test1",
                PhoneNumber = "123",
            };

            //act & assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                sut.CreateBarAsync(bar).GetAwaiter();
                Assert.IsTrue(sut.BarWithThatNameExists(bar.Name).GetAwaiter().GetResult());
            }
        }

        [TestMethod]
        public void ReturnFalseIfSuchBarDoesntExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalseIfSuchBarDoesntExist));

            //act & assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                Assert.IsFalse(sut.BarWithThatNameExists("test").GetAwaiter().GetResult());
            }
        }
    }
}
