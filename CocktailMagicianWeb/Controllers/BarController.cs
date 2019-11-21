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

        public BarController(IBarServices barServices, ICocktailServices cocktailServices)
        {
            _barServices = barServices;
            _cocktailServices = cocktailServices;
        }

        [HttpGet]
        [Authorize(Roles = "CocktailMagician")]
        public IActionResult CreateBar()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "CocktailMagician")]
        public async Task<IActionResult> CreateBar(BarViewModel barmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var coctailsList = new List<Cocktail>(); 
            var barModel = await barmodel.MapToModel();
            foreach (var cocktail in barmodel.Cocktails)
            {
                
            }
            await this._barServices.CreateBarAsync(barModel);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ListBars()
        {
            var barsResult = new BarResultsViewModel();
            barsResult.Bars = (await _barServices.GetVisibleCollectionAsync()).Select(b => b.MapToViewModel()).ToList();
            return View(barsResult);
        }
        [HttpGet]
        public async Task<IActionResult> SearchBars()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchBars(BarSearchViewModel viewModel)
        {

            viewModel.SearchResults = (await this._barServices.SearchBarsByMultipleCriteriaAsync(viewModel.Name, viewModel.Address, viewModel.PhoneNumber)).Select(b => b.MapToViewModel()).ToList();
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
            var bar = await viewModel.MapToModel();
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
    }
}