using CocktailMagician.Data;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Tests
{
    public static class TestUtilities
    {

        public static DbContextOptions<CocktailDB> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<CocktailDB>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
