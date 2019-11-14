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
        public List<BarReview> BarReviews { get; set; }
        public List<CocktailReview> CocktaReviews { get; set; }
    }
}
