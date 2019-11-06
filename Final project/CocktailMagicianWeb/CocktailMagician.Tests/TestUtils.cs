using CocktailMagician.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Tests
{
    public static class TestUtils
    {
        public static DbContextOptions<CocktailDB> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<CocktailDB>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}

