using CocktailMagician.Data.Entities;
using CocktailMagicianWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            viewmodel.CurrentPicture = bar.Picture;
            viewmodel.BarReviews = bar.BarReviews;
            try
            {
                viewmodel.Rating = bar.BarReviews.Select(b => b.Rating).Average();
            }
            catch (Exception)
            {
                viewmodel.Rating = 0;
            }
            viewmodel.Cocktails = bar.BarCocktails.Select(b => b.Cocktail.Name).ToList();
            return viewmodel;
        }
        public async static Task<Bar> MapToModel(this BarViewModel viewModel)
        {
            var bar = new Bar();
            bar.Id = viewModel.Id;
            bar.Name = viewModel.Name;
            bar.Address = viewModel.Address;
            bar.PhoneNumber = viewModel.PhoneNumber;
            if(viewModel.NewPicture != null)
            {
                using (var stream = new MemoryStream())
                {
                    await viewModel.NewPicture.CopyToAsync(stream);
                    viewModel.CurrentPicture = stream.ToArray();
                }
            }
            bar.Picture = viewModel.CurrentPicture;

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
