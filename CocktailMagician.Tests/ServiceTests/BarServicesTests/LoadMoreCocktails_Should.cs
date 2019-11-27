using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.BarServicesTests
{
    [TestClass]
    public class LoadMoreCocktails_Should
    {
        [TestMethod]
        public void ReturnCollectionOfCorrectCocktailNames()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfCorrectCocktailNames));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new BarServices(assertContext);

                var bar = new Bar
                {
                    Name = "Barsy",
                    Id = 1
                };

                var cocktail1 = new Cocktail
                {
                    Name = "Mojito1",
                    Id = 1
                };

                var cocktail2 = new Cocktail
                {
                    Name = "Mojito2",
                    Id = 2
                };

                var cocktail3 = new Cocktail
                {
                    Name = "Mojito3",
                    Id = 3
                };

                var pair1 = new BarCocktail
                {
                    Bar = bar,
                    BarID = bar.Id,
                    Cocktail = cocktail1,
                    CocktailID = cocktail1.Id
                };
                var pair2 = new BarCocktail
                {
                    Bar = bar,
                    BarID = bar.Id,
                    Cocktail = cocktail2,
                    CocktailID = cocktail2.Id
                };
                var pair3 = new BarCocktail
                {
                    Bar = bar,
                    BarID = bar.Id,
                    Cocktail = cocktail3,
                    CocktailID = cocktail3.Id
                };

                assertContext.BarCocktail.Add(pair1);
                assertContext.BarCocktail.Add(pair2);
                assertContext.BarCocktail.Add(pair3);
                assertContext.SaveChanges();

                var result = sut.LoadMoreCocktails(1, bar.Id).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 2 && result[0] == cocktail2.Name && result[1] == cocktail3.Name );
            }
        }
    }
}
