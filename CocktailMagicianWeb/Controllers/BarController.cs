using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CocktailMagicianWeb.Utilities.Mappers;
using CocktailMagicianWeb.Models;
using Microsoft.AspNetCore.Authorization;
using CocktailMagicianWeb.Models.Bars;

namespace CocktailMagicianWeb.Controllers
{
    public class BarController : Controller
    {
        private readonly IBarServices _barServices;
        private readonly ICocktailServices _cocktailServices;
        private readonly IBarCocktailServices _barCocktailServices;

        public BarController(IBarServices barServices, ICocktailServices cocktailServices, IBarCocktailServices barCocktailServices)
        {
            _barServices = barServices;
            _cocktailServices = cocktailServices;
            _barCocktailServices = barCocktailServices;
        }

        [HttpGet]
        [Authorize(Roles = "CocktailMagician")]
        public IActionResult CreateBar()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> CreateBar(BarViewModel barViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var barModel = await barViewModel.MapToEntity();
            await _barServices.CreateBarAsync(barModel);
            foreach (var cocktail in barViewModel.Cocktails)
            {
                var cocktailEntity = await _cocktailServices.GetAsync(cocktail);
                await _barCocktailServices.CreateAsync(barModel, cocktailEntity);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "CocktailMagician")]

        public async Task<IActionResult> ManageBars(BarSearchViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Name))
            {
                return View();
            }

            vm.SearchResults = (await _barServices.GetMultipleBarsByNameAsync(vm.Name)).Select(b => b.MapToViewModel()).ToList();
            if (vm.SearchResults.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "No bars found with this name.");
                return View();
            }

            return View(vm);
        }
        public IActionResult TestView()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchBars()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchBars(BarSearchViewModel viewModel)
        {
            if (viewModel.AvgRating != null)
            {
                viewModel.SearchResults = (await this._barServices.SearchBarsByMultipleCriteriaAsync(viewModel.Name, viewModel.Address, viewModel.PhoneNumber, viewModel.ShowOnlyHiddenFiles)).Select(b => b.MapToViewModel()).Where(b => b.Rating == viewModel.AvgRating).ToList();
            }
            else
            {
                viewModel.SearchResults = (await this._barServices.SearchBarsByMultipleCriteriaAsync(viewModel.Name, viewModel.Address, viewModel.PhoneNumber, viewModel.ShowOnlyHiddenFiles)).Select(b => b.MapToViewModel()).ToList();
            }
            return View(viewModel);

        }
        [HttpGet]
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> EditBar(int id)
        {
            var viewmodel = (await this._barServices.GetBarAsync(id)).MapToViewModel();

            return View(viewmodel);
        }
        [HttpPost]
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> EditBar(BarViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var bar = await viewModel.MapToEntity();
            await this._barServices.EditBarAsync(bar);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> BarDetails(int Id)
        {
            var bar = await this._barServices.GetBarAsync(Id);
            var viewModel = bar.MapToViewModel();
            return View(viewModel);
        }

        public async Task<IActionResult> GetBars(string type)
        {
            var bars = await _barServices.GetCollectionAsync();
            return Json(bars);
        }
        public async Task<IActionResult> LoadMoreCocktails([FromQuery]int Loaded, [FromQuery]int id)
        {
            var cocktails = await _barServices.LoadMoreCocktails(Loaded, id);
            return Json(cocktails);
        }
        public async Task HideBar([FromBody]BarViewModel vm)
        {
            await _barServices.HideAsync(vm.Id);
        }

        public async Task UnhideBar([FromBody]BarViewModel vm)
        {
            await _barServices.UnhideAsync(vm.Id);
        }
    }
}