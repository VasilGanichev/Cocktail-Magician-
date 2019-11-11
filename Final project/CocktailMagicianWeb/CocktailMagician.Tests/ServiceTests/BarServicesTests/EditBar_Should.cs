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
    public class EditBar_Should
    {
        [TestMethod]
        public void CorrectlyEditBar()
        {
            var options = TestUtils.GetOptions(nameof(CorrectlyEditBar));
            var resultName = "test1";
            var resultAddress = "Mill Street";
            var resultPhoneNumber = "testNumber";
            var bar = new Bar
            {
                Id = 1,
                Name = "asd",
                Address ="asd",
                PhoneNumber = "add",
                IsHidden = false,
            };
            using(var actContext = new CocktailDB(options))
            {
                var sut = new BarServices(actContext);
                actContext.Bars.AddAsync(bar).GetAwaiter();
                actContext.SaveChangesAsync().GetAwaiter();
                sut.EditBarAsync(bar, resultName, resultAddress, resultPhoneNumber, null, true).GetAwaiter();
            }
            using (var asertContext = new CocktailDB(options))
            {
                var sut = new BarServices(asertContext);
                var resultBar = sut.GetBarAsync(bar.Id).GetAwaiter().GetResult();
                Assert.AreEqual(resultBar.Name, resultName);
                Assert.AreEqual(resultBar.Address, resultAddress);
                Assert.AreEqual(resultBar.PhoneNumber, resultPhoneNumber);
                Assert.IsTrue(resultBar.IsHidden);
            }
        }
    }
}
