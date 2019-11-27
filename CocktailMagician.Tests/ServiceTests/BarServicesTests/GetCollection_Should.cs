using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CocktailMagician.Tests.ServiceTests.BarServicesTests
{
    [TestClass]
    public class GetCollection_Should
    {
        [TestMethod]
        public void ReturnCollectionOfCorrectBars()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfCorrectBars));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var bar = new Bar
                {
                    Id = 1,
                    Name = "test",
                    Address = "asasas",
                    PhoneNumber = "asd",
                };

                var bar1 = new Bar
                {
                    Id = 2,
                    Name = "test1",
                    Address = "asasas",
                    PhoneNumber = "asd",
                };

                assertContext.Bars.Add(bar);
                assertContext.Bars.Add(bar1);
                assertContext.SaveChanges();

                var result = sut.GetCollectionAsync().GetAwaiter().GetResult();
                Assert.IsTrue(result.Count == 2 && result.Contains(bar) && result.Contains(bar1));
            }
        }
        [TestMethod]
        public void ReturnEmptyCollectionIfNoBarsExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionIfNoBarsExist));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var result = sut.GetCollectionAsync().GetAwaiter().GetResult();
                Assert.IsTrue(result.Count == 0);
            }
        }
    }
}
