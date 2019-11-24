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
    public class CreateBar_Should
    {
      [TestMethod]
      public void ThrowWhenBarIsNull()
      {
          var options = TestUtilities.GetOptions(nameof(ThrowWhenBarIsNull));
          using (var assertContext = new CocktailDB(options))
          {
              var sut = new BarServices(assertContext);
              Assert.ThrowsExceptionAsync<ArgumentNullException>(()=>sut.CreateBarAsync(null)
                  );
          }
      }
      [TestMethod]
      public void ThrowWhenParametersAreNull()
      {
        var options = TestUtilities.GetOptions(nameof(ThrowWhenParametersAreNull));
        var inCorrectBar = new Bar
        {
            Name = null,
            Address = null,
            PhoneNumber = null,

        };
        //act, assert
        using (var assertContext = new CocktailDB(options))
        {
            var sut = new BarServices(assertContext);
            Assert.ThrowsExceptionAsync<ArgumentNullException>(()    =>sut.CreateBarAsync(inCorrectBar));
            Assert.ThrowsExceptionAsync<ArgumentNullException>(()    =>sut.CreateBarAsync(null, null, null, null)
            );
        }
      }
      [TestMethod]
      public void AddBarToDB()
      {
          var options = TestUtilities.GetOptions(nameof(AddBarToDB));
            var bar = new Bar
            {
                Id = 1,
                Name = "test",
                Address = "asasas",
                PhoneNumber = "asd",
            };
    
            //act & assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                sut.CreateBarAsync(bar).GetAwaiter();
                Assert.IsTrue(assertContext.Bars.Contains(bar));
            }
        }
    }
}
