using CocktailMagician.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

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

