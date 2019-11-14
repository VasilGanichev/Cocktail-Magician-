using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class BarReview
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public int BarId { get; set; }
        public Bar Bar { get; set; }

    }
}
