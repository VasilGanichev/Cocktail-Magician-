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
    public class SearchByMultipleCriteria_Should
    {
        [TestMethod]
        public void ReturnCollectionWhenParametersAreNull()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionWhenParametersAreNull));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "number";
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
            using (var arrangeContext = new CocktailDB(options))
            {
                arrangeContext.Bars.AddAsync(bar).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar2).GetAwaiter();
                arrangeContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var collection = sut.SearchBarsByMultipleCriteriaAsync(null, null, null, false).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(2, collection.Count);


            }

        }
        [TestMethod]
        public void ReturnEmptyCollectionWhenParametersAreNotFound()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionWhenParametersAreNotFound));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "number";
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
            using (var arrangeContext = new CocktailDB(options))
            {
                arrangeContext.Bars.AddAsync(bar).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar2).GetAwaiter();
                arrangeContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var collection = sut.SearchBarsByMultipleCriteriaAsync("kon", null, null, false).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(0, collection.Count);

            }
        }
        [TestMethod]
        public void ReturnCorrectBarsWithNameParam()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectBarsWithNameParam));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "number";
            var bar = new Bar
            {
                Id = -1,
                Name = "kon",
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
            using (var arrangeContext = new CocktailDB(options))
            {
                arrangeContext.Bars.AddAsync(bar).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar2).GetAwaiter();
                arrangeContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var collection = sut.SearchBarsByMultipleCriteriaAsync("kon", null, null, false).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(1, collection.Count);

            }
        }
        [TestMethod]
        public void ReturnCorrectBarsWithAdressParam()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectBarsWithAdressParam));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "number";
            var bar = new Bar
            {
                Id = -1,
                Name = name,
                Address = "kon",
                PhoneNumber = phoneNumber,

            };
            var bar2 = new Bar
            {
                Id = -2,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,

            };
            using (var arrangeContext = new CocktailDB(options))
            {
                arrangeContext.Bars.AddAsync(bar).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar2).GetAwaiter();
                arrangeContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var collection = sut.SearchBarsByMultipleCriteriaAsync(null, "kon", null, false).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(1, collection.Count);
                Assert.AreEqual(bar.Id, collection.First().Id);
                Assert.AreEqual(bar.Address, collection.First().Address);
                Assert.AreEqual(bar.Name, collection.First().Name);
                Assert.AreEqual(bar.PhoneNumber, collection.First().PhoneNumber);
            }
        }
        [TestMethod]
        public void ReturnCorrectBarsWithPhoneNumberParam()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectBarsWithPhoneNumberParam));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "number";
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
                PhoneNumber = "kon",

            };
            using (var arrangeContext = new CocktailDB(options))
            {
                arrangeContext.Bars.AddAsync(bar).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar2).GetAwaiter();
                arrangeContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var collection = sut.SearchBarsByMultipleCriteriaAsync(null, null, "kon", false).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(1, collection.Count);
                Assert.AreEqual(bar2.Id, collection.First().Id);
                Assert.AreEqual(bar2.Address, collection.First().Address);
                Assert.AreEqual(bar2.Name, collection.First().Name);
                Assert.AreEqual(bar2.PhoneNumber, collection.First().PhoneNumber);
            }
        }
        [TestMethod]
        public void ReturnOnlyHiddenFiles()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnOnlyHiddenFiles));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "number";
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
                IsHidden = true,
            };

            using (var arrangeContext = new CocktailDB(options))
            {
                arrangeContext.Bars.AddAsync(bar).GetAwaiter();
                arrangeContext.Bars.AddAsync(bar2).GetAwaiter();
                arrangeContext.SaveChangesAsync().GetAwaiter();
            }
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);
                var collection = sut.SearchBarsByMultipleCriteriaAsync(null, null, null, true).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(1, collection.Count);
                Assert.AreEqual(bar2.Id, collection.First().Id);
                Assert.AreEqual(bar2.Address, collection.First().Address);
                Assert.AreEqual(bar2.Name, collection.First().Name);
                Assert.AreEqual(bar2.PhoneNumber, collection.First().PhoneNumber);
                Assert.AreEqual(bar2.IsHidden, collection.First().IsHidden);
            }
        }
        [TestMethod]
        public void ReturnAllBarsContainingNameCharacter()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnAllBarsContainingNameCharacter));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "number";
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
                Name = "ko staa",
                Address = address,
                PhoneNumber = phoneNumber,
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
                var collection = sut.SearchBarsByMultipleCriteriaAsync("e", null, null, false).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(2, collection.Count);
               
            }
        }
        [TestMethod]
        public void ReturnAllBarsContainingAdressCharacter()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnAllBarsContainingAdressCharacter));
            string name = "test";
            string address = "TestAddr";
            string phoneNumber = "number";
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
                PhoneNumber = phoneNumber,
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
                var collection = sut.SearchBarsByMultipleCriteriaAsync(null, "t", null, false).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(2, collection.Count);

            }
        }
        [TestMethod]
        public void ReturnAllBarsContainingNumberCharacter()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnAllBarsContainingNumberCharacter));
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
                var collection = sut.SearchBarsByMultipleCriteriaAsync(null, null, "12", false).GetAwaiter().GetResult();
                Assert.IsNotNull(collection);
                Assert.AreEqual(2, collection.Count);

            }
        }
    }

}
