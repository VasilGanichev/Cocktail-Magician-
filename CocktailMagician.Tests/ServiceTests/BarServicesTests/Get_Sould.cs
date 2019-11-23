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
    public class Get_Sould
    {
        [TestMethod]
        public void ReturnNullWhenNameIsNotFound()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnNullWhenNameIsNotFound));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "123";
            var bar = new Bar
            {
                Id = -1,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,

            };
            var bar2 = new Bar
            {
                Id = -2,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
            };
            var bar3 = new Bar
            {
                Id = -3,
                Name = name,
                Address = "xdr",
                PhoneNumber = "asd",
            };

            using (var arrangeContext = new CocktailDB(options))
            {
                arrangeContext.Bars.AddAsync(bar).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar2).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar3).GetAwaiter();

                arrangeContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var result = sut.GetAsync("asassaasasassaasas").GetAwaiter().GetResult();
                Assert.IsNull(result);

            }
        }
        [TestMethod]
        public void ReturnFirstCorrectName()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFirstCorrectName));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "123";
            var bar = new Bar
            {
                Id = -1,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,

            };
            var bar2 = new Bar
            {
                Id = -2,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
            };
            var bar3 = new Bar
            {
                Id = -3,
                Name = name,
                Address = "xdr",
                PhoneNumber = "asd",
            };

            using (var arrangeContext = new CocktailDB(options))
            {
                arrangeContext.Bars.AddAsync(bar).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar2).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar3).GetAwaiter();

                arrangeContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var result = sut.GetAsync(name).GetAwaiter().GetResult();
                Assert.IsNotNull(result);
                Assert.AreEqual(bar.Id, result.Id);

            }
        }
        [TestMethod]
        public void ThrowWhenParametersAreNull()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFirstCorrectName));

            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetAsync(null));
            }
        }
    }
}
