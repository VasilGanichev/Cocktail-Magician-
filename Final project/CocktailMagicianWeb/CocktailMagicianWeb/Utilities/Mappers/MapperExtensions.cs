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
            var viewmodel = new BarViewModel
            {
                Name = bar.Name,
                Address = bar.Address,
                PhoneNumber = bar.PhoneNumber,
                Picture = bar.Picture,

            };
            return viewmodel;
        }
    }
}
