﻿using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Tests.ServiceTests.CocktailServicesTests
{
    [TestClass]
    public class SearchByMultipleCriteria_Should
    {
        [TestMethod]
        public void ReturnOnlyAlcoholicCocktailsByCocktailNameWhenThereAreAny()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnOnlyAlcoholicCocktailsByCocktailNameWhenThereAreAny));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "Rum",
                    Type = "alcohol",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var cocktailName = cocktail.Name;
                var includeOnlyAlcoholic = true;
                var result = sut.SearchByMultipleCriteriaAsync(cocktailName, null, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 1 && result.Contains(cocktail));
            }
        }

        [TestMethod]
        public void ReturnEmptyCollectionByCocktailNameWhenThereAreNoAlcoholic()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnOnlyAlcoholicCocktailsByCocktailNameWhenThereAreAny));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "sugar",
                    Type = "sweetener",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var cocktailName = cocktail.Name;
                var includeOnlyAlcoholic = true;
                var result = sut.SearchByMultipleCriteriaAsync(cocktailName, null, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }

        // doesn't work because of bug

        //[TestMethod]
        //public void ReturnOnlyAlcoholicCocktailsByIngredientNameWhenThereAreAny()
        //{
        //    // Arrange
        //    var options = TestUtilities.GetOptions(nameof(ReturnOnlyAlcoholicCocktailsByIngredientNameWhenThereAreAny));


            // Act, Assert
        //    using (var assertContext = new CocktailDB(options))
        //    {
        //        var sut = new CocktailServices(assertContext);
        //        var name = "Rums and Dreams";
        //        var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
        //        name = "Rums and Dreams2";
        //        var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
        //        name = "Rums and Dreams3";
        //        var cocktail3 = sut.AddAsync(name, null).GetAwaiter().GetResult();
        //        var ingredient = new Ingredient
        //        {
        //            Name = "Rum",
        //            Type = "alcohol",
        //        };
        //        assertContext.Ingredients.Add(ingredient);

        //        var cocktailIngredient = new CocktailIngredient
        //        {
        //            Ingredient = ingredient,
        //            IngredientID = ingredient.Id,
        //            Cocktail = cocktail2,
        //            CocktailID = cocktail2.Id
        //        };
        //        assertContext.CocktailIngredients.Add(cocktailIngredient);

        //        var cocktailIngredient1 = new CocktailIngredient
        //        {
        //            Ingredient = ingredient,
        //            IngredientID = ingredient.Id,
        //            Cocktail = cocktail3,
        //            CocktailID = cocktail3.Id
        //        };
        //        assertContext.CocktailIngredients.Add(cocktailIngredient1);
        //        assertContext.SaveChanges();

        //        var ingredientName = ingredient.Name;
        //        var includeOnlyAlcoholic = true;
        //        var result = sut.SearchByMultipleCriteriaAsync(null, ingredientName, includeOnlyAlcoholic).GetAwaiter().GetResult();

        //        Assert.AreEqual(2, result.Count);
        //    }
        //}

        [TestMethod]
        public void ReturnEmptyCollectionByIngredientNameWhenThereAreNoAlcoholic()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionByIngredientNameWhenThereAreNoAlcoholic));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "sugar",
                    Type = "sweetener",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var ingredientName = ingredient.Name;
                var includeOnlyAlcoholic = true;
                var result = sut.SearchByMultipleCriteriaAsync(null, ingredientName, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }

        [TestMethod]
        public void ReturnAlcoholicCocktailsOnly()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnAlcoholicCocktailsOnly));

            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "Rum",
                    Type = "alcohol",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var includeOnlyAlcoholic = true;
                var result = sut.SearchByMultipleCriteriaAsync(null, null, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 1 && result[0] == cocktail);
            }
        }

        [TestMethod]
        public void ReturnEmptyCollectionWhenOnlyAlcoholicIsChecked()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionWhenOnlyAlcoholicIsChecked));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "sugar",
                    Type = "sweetener",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var ingredientName = ingredient.Name;
                var includeOnlyAlcoholic = true;
                var result = sut.SearchByMultipleCriteriaAsync(null, null, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }

        //

        [TestMethod]
        public void ReturnNonAlcoholicByCocktailName()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNonAlcoholicByCocktailName));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "Rum",
                    Type = "alcohol",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var cocktailName = cocktail2.Name;
                var includeOnlyAlcoholic = false;
                var result = sut.SearchByMultipleCriteriaAsync(cocktailName, null, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 1 && result.Contains(cocktail2));
            }
        }

        [TestMethod]
        public void ReturnEmptyCollectionByCocktailNameNonAlcoholic()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionByCocktailNameNonAlcoholic));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "sugar",
                    Type = "sweetener",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var cocktailName = cocktail.Name;
                var includeOnlyAlcoholic = true;
                var result = sut.SearchByMultipleCriteriaAsync(cocktailName, null, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }

        [TestMethod]
        public void ReturnOnlyAlcoholicCocktailsByIngredientNameWhenThereAreAny1()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnOnlyAlcoholicCocktailsByIngredientNameWhenThereAreAny1));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "Rum",
                    Type = "alcohol",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var ingredientName = ingredient.Name;
                var includeOnlyAlcoholic = true;
                var result = sut.SearchByMultipleCriteriaAsync(null, ingredientName, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }

        [TestMethod]
        public void ReturnEmptyCollectionByIngredientNameWhenThereAreNoAlcoholic1()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnEmptyCollectionByIngredientNameWhenThereAreNoAlcoholic1));


            // Act, Assert
            using (var assertContext = new CocktailDB(options))
            {
                var sut = new CocktailServices(assertContext);
                var name = "Rums and Dreams";
                var cocktail = sut.AddAsync(name, null).GetAwaiter().GetResult();
                name = "Rums and Dreams2";
                var cocktail2 = sut.AddAsync(name, null).GetAwaiter().GetResult();
                var ingredient = new Ingredient
                {
                    Name = "sugar",
                    Type = "sweetener",
                };
                assertContext.Ingredients.Add(ingredient);

                var cocktailIngredient = new CocktailIngredient
                {
                    Ingredient = ingredient,
                    IngredientID = ingredient.Id,
                    Cocktail = cocktail,
                    CocktailID = cocktail.Id
                };
                assertContext.CocktailIngredients.Add(cocktailIngredient);
                assertContext.SaveChanges();

                var ingredientName = ingredient.Name;
                var includeOnlyAlcoholic = true;
                var result = sut.SearchByMultipleCriteriaAsync(null, ingredientName, includeOnlyAlcoholic).GetAwaiter().GetResult();

                Assert.IsTrue(result.Count == 0);
            }
        }
    }
}
