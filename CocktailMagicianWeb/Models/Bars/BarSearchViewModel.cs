using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Models
{
    public class BarSearchViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public double? AvgRating { get; set; }
        public bool ShowOnlyHiddenFiles { get; set; }
        public IReadOnlyCollection<BarViewModel> SearchResults { get; set; }
    }
}
