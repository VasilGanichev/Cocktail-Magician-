using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.BarServicesTests
{
    [TestClass]
    public class GetMultipleBarsByName_Should
    {
        [TestMethod]
        public void ReturnCollectionOfCorrectBars()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfCorrectBars));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var bar1 = new Bar
                {
                    Name = "FF1"
                };
                var bar2 = new Bar
                {
                    Name = "FF2"
                };

                assertContext.Bars.Add(bar1);
                assertContext.Bars.Add(bar2);
                assertContext.SaveChanges();

                var result = sut.GetMultipleBarsByNameAsync("F").GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 2 && result.Contains(bar1) && result.Contains(bar2));
            }
        }

        [TestMethod]
        public void ReturnEmptyCollectionIfNoBarsAreFound()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionIfNoBarsAreFound));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var result = sut.GetMultipleBarsByNameAsync("u").GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }

    }
}
