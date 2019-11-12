using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailMagician.Tests.ServiceTests.BarServicesTests
{
    [TestClass]
    public class GetBar_Should
    {
        [TestMethod]
        public void ThrowWhenIdNotFound()
        {
            var options = TestUtils.GetOptions(nameof(ThrowWhenIdNotFound));

            var testBar = new Bar
            {
                Name = "Test",
                Address = "Test",
                PhoneNumber = "Test",
            };

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);

                sut.CreateBarAsync(testBar).GetAwaiter();
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetBarAsync(int.MaxValue));

            }
        }
        [TestMethod]
        public void ReturnCorrectBar()
        {
            var options = TestUtils.GetOptions(nameof(ReturnCorrectBar));

            var testBar = new Bar
            {
                Id =1,
                Name = "Test",
                Address = "Test",
                PhoneNumber = "Test",
            };
            // Act
            using (var actContext = new CocktailDB(options))
            {
            
                var sut = new BarServices(actContext);
                actContext.Bars.Add(testBar);
                actContext.SaveChangesAsync().GetAwaiter();
            }
            // Assert
            using (var assertContext = new CocktailDB(options))
            {
                var id = 1;
                var sut = new BarServices(assertContext);
                var result = sut.GetBarAsync(id).GetAwaiter().GetResult();
                Assert.IsTrue(assertContext.Bars.Contains(testBar));
                Assert.AreEqual(testBar.Id, result.Id);

            }
        }
    }
}
