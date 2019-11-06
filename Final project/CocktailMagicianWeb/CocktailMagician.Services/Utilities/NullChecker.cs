using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Utilities
{
    public static class NullChecker
    {
        public static void EnsureNotNull(this object o)
        {
            if (o == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
