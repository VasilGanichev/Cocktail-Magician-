using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Tests.ServiceTests.BarServicesTests
{
    [TestClass]
    public class HideBar_Should
    {
        [TestMethod]
        public void ThrowWhenBarIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowWhenBarIsNull));
            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                sut.HideBarAsync(null));
            }
        }
        [TestMethod]
        public void ThrowWhenBarIsHidden()
        {
            var options = TestUtils.GetOptions(nameof(ThrowWhenBarIsHidden));
            var bar = new Bar
            {
                Id = 1,
                IsHidden = true,
            };
            // Act
            using (var actContext = new CocktailDB(options))
            {
                actContext.Bars.AddAsync(bar).GetAwaiter();
                actContext.SaveChangesAsync().GetAwaiter();
            }
            // Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                sut.HideBarAsync(bar));
            }
        }
        [TestMethod]
        public void ThrowWhenBarIsNotFound()
        {
            var options = TestUtils.GetOptions(nameof(ThrowWhenBarIsNotFound));
            var bar = new Bar
            {
                Id = 1,
            };
            var bar2 = new Bar
            {
                Id = 2,
            };
            // Act
            using (var actContext = new CocktailDB(options))
            {
                actContext.AddAsync(bar).GetAwaiter();
                actContext.SaveChangesAsync().GetAwaiter();
            }
            // Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                sut.HideBarAsync(bar2));
            }
        }
        [TestMethod]
        public void HideBar()
        {
            var options = TestUtils.GetOptions(nameof(HideBar));
            var bar = new Bar
            {
                Id = 2,
                IsHidden = false,
            };
            // Act
            using (var actContext = new CocktailDB(options))
            {
                var sut = new BarServices(actContext);
                actContext.AddAsync(bar).GetAwaiter();
                actContext.SaveChangesAsync().GetAwaiter();
                sut.HideBarAsync(bar).GetAwaiter();

            }
            // Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var result = sut.GetBarAsync(bar.Id).GetAwaiter().GetResult();
                Assert.IsTrue(result.IsHidden);
            }
        }
    }
}
