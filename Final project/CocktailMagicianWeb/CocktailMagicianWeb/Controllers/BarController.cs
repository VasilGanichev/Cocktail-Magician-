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
        public async  Task<IActionResult> CreateBar(Bar bar, List<IFormFile> Picture) 
        {
            foreach (var item in Picture)
            {
                if(item.Length>0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        bar.Picture = stream.ToArray();
                    }
                }
            }
            await this.barServices.CreateBarAsync(bar);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ListBars()
        {
            var barsResult = new BarResultsViewModel();
             barsResult.Bars = (await barServices.GetVisibleCollectionAsync()).Select(b => b.MapToViewModel()).ToList();
            return View(barsResult);
        }
    }
}