using CocktailMagician.Data.Entities;
using CocktailMagicianWeb.Models;
using CocktailMagicianWeb.Models.Cocktails;
using CocktailMagicianWeb.Models.Ingredients;
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
                viewmodel.Rating = Math.Round(bar.BarReviews.Select(b => b.Rating).Average(),2);
            }
            catch (Exception)
            {
                viewmodel.Rating = 0;
            }
            try
            {
                viewmodel.Cocktails = bar.BarCocktails.Select(b => b.Cocktail.Name).Take(10).ToList();
            }
            catch (Exception)
            {
                viewmodel.Cocktails = null;
            }
            return viewmodel;
        }

        public async static Task<Bar> MapToEntity(this BarViewModel viewModel)
        {
            if (viewModel.NewPicture != null)
            {
                using (var stream = new MemoryStream())
                {
                    await viewModel.NewPicture.CopyToAsync(stream);
                    viewModel.CurrentPicture = stream.ToArray();
                }
            }   
            var bar = new Bar()
            {
                Name = viewModel.Name,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Picture = viewModel.CurrentPicture,
                IsHidden = viewModel.IsHidden,
            };
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
        public static IngredientViewModel MapToViewModel(this Ingredient ingredient)
        {
            var vm = new IngredientViewModel();
            vm.ID = ingredient.Id;
            vm.Name = ingredient.Name;
            vm.Type = ingredient.Type;
            return vm;
        }
        public static UpdateIngredientViemModel MapToUpdateViewModel(this Ingredient ingredient)
        {
            var vm = new UpdateIngredientViemModel();
            vm.ID = ingredient.Id;
            vm.CurrentName = ingredient.Name;
            return vm;
        }

        public static CocktailViewModel MapToViewModel(this Cocktail cocktail)
        {
            var vm = new CocktailViewModel();
            vm.Id = cocktail.Id;
            vm.Name = cocktail.Name;
            vm.IsHidden = cocktail.IsHidden;
            vm.Picture = cocktail.Picture;
            vm.CocktailReviews = cocktail.CocktailReviews;
            try
            {
                vm.Rating = Math.Round(cocktail.CocktailReviews.Select(b => b.Rating).Average(),2);
            }
            catch (Exception)
            {
                vm.Rating = 0;
            }
            try
            {
                vm.Bars = cocktail.Bars.Select(b => b.Bar.Name).ToList();
            }
            catch (Exception)
            {
                vm.Bars = null;
            }
            try
            {
                vm.Ingredients = cocktail.Ingredients.Select(b => b.Ingredient.Name).ToList();
            }
            catch (Exception)
            {
                vm.Ingredients = null;
            }
            return vm;
        }

        public static UpdateCocktailViewModel MapToViewUpdateModel(this Cocktail cocktail, IReadOnlyCollection<Bar> allBars)
        {
            var vm = new UpdateCocktailViewModel();
            vm.Id = cocktail.Id;
            vm.Name = cocktail.Name;
            vm.IsHidden = cocktail.IsHidden;
            vm.Picture = cocktail.Picture;
            vm.Quantities = cocktail.Ingredients.Select(i => i.Quantity).ToList();
            vm.CurrentIngredients = cocktail.Ingredients.Select(c => c.Ingredient.MapToViewModel()).ToList();
            vm.AllBars = allBars.Select(b => b.Name).ToList();
            if (cocktail.Bars.Count != 0)
            {
                vm.OfferingBars = cocktail.Bars.Select(c => c.Bar.Name).ToList();
            }

            return vm;
        }

        public static Cocktail MapToEntity(this UpdateCocktailViewModel vm)
        {
            var cocktail = new Cocktail
            {
                Id = vm.Id,
                Name = vm.Name,
                IsHidden = vm.IsHidden,
                Picture = vm.Picture
            };

            return cocktail;
        }

        public static Ingredient MapToEntity(this IngredientViewModel vm)
        {
            var ingredient = new Ingredient
            {
                Id = vm.ID,
                Name = vm.Name,
                Type = vm.Type
            };

            return ingredient;
        }
    }
}
