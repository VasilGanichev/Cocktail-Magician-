using CocktailMagician.Data.Entities;
using CocktailMagicianWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Utilities.Mappers
{
    public static class MapperExtensions
    {
        public static BarViewModel MapToViewModel(this Bar bar)
        {
            var viewmodel = new BarViewModel();
            viewmodel.Id = bar.Id;
            viewmodel.Name = bar.Name;
            viewmodel.Address = bar.Address;
            viewmodel.PhoneNumber = bar.PhoneNumber;
            viewmodel.Picture = bar.Picture;
            viewmodel.BarReviews = bar.BarReviews;
            try
            {
                viewmodel.Rating = bar.BarReviews.Select(b => b.Rating).Average();
            }
            catch (Exception)
            {
                viewmodel.Rating = 0;
            }
            return viewmodel;
        }
    }
}
