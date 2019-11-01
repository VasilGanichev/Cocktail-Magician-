using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class User : IdentityUser
    {
        public bool IsBanned { get; set; }
        public int ReviewId { get; set; }
        public List<Review>Reviews { get; set; }
    }
}
