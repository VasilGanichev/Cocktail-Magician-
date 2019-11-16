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
        public static Bar MapToModel(this BarViewModel viewModel)
        {
            var bar = new Bar();
            bar.Id = viewModel.Id;
            bar.Name = viewModel.Name;
            bar.Address = viewModel.Address;
            bar.PhoneNumber = viewModel.PhoneNumber;
            bar.Picture = viewModel.Picture;

            return bar;
        }
        public static UserViewModel MapToViewmodel(this User user, string userRole)
        {
            var viewModel = new UserViewModel();
            viewModel.UserId = user.Id;
            viewModel.Name = user.UserName;
            viewModel.Role = userRole;
            viewModel.IsBanned = user.IsBanned;
            return viewModel;
        }
    }
}
