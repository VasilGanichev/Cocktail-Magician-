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

namespace CocktailMagicianWeb.Controllers
{
    public class BarController : Controller
    {
        private readonly IBarServices barServices;

        public BarController(IBarServices barServices)
        {
            this.barServices = barServices;
        }

        [HttpGet]
        public IActionResult CreateBar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBar(BarViewModel bar, List<IFormFile> Picture)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            foreach (var item in Picture)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        bar.Picture = stream.ToArray();
                    }
                }
            }
            var barModel = bar.MapToModel();
            await this.barServices.CreateBarAsync(barModel);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ListBars()
        {
            var barsResult = new BarResultsViewModel();
            barsResult.Bars = (await barServices.GetVisibleCollectionAsync()).Select(b => b.MapToViewModel()).ToList();
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

            viewModel.SearchResults = (await this.barServices.SearchBooksByMultipleCriteriaAsync(viewModel.Name, viewModel.Address, viewModel.PhoneNumber)).Select(b => b.MapToViewModel()).ToList();
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> EditBar(int id)
        {
            var viewmodel = (await this.barServices.GetBarAsync(id)).MapToViewModel();

            return View(viewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> EditBar(BarViewModel viewModel, List<IFormFile> Picture)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            foreach (var item in Picture)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        viewModel.Picture = stream.ToArray();
                    }
                }
            }
            var bar = viewModel.MapToModel();
            await this.barServices.EditBarAsync(bar);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> BarDetails(int Id)
        {
            var bar = await this.barServices.GetBarAsync(Id);
            var viewModel = bar.MapToViewModel();
            return View(viewModel);
        }
    }
}