using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CocktailMagician.Tests.ServiceTests.BarServicesTests
{
    [TestClass]
    public class LoadNewestBars_Should
    {
        [TestMethod]
        public void ReturnCollectionOf10NewestBarsOrderedAscending()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOf10NewestBarsOrderedAscending));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var bar1 = new Bar
                {
                    Name = "FF1",
                    CreatedOn = DateTime.Now

                };
                var bar2 = new Bar
                {
                    Name = "FF2",
                    CreatedOn = DateTime.Now
                };

                assertContext.Bars.Add(bar1);
                assertContext.Bars.Add(bar2);
                assertContext.SaveChanges();

                var result = sut.LoadNewestBars().GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 2 && result[0] == bar1 && result[1] == bar2);
            }
        }
        [TestMethod]
        public void ReturnEmptyCollectionIfThereAreNoBars()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionIfThereAreNoBars));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var result = sut.LoadNewestBars().GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }
    }
}
