using CocktailMagician.Services.Contracts;
using CocktailMagicianWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CocktailMagicianWeb.Utilities.Mappers;
using System.Threading.Tasks;
using System.Linq;
using CocktailMagicianWeb.Models.Home;

namespace CocktailMagicianWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBarServices _barServices;
        private readonly ICocktailServices _cocktailServices;

        public HomeController(IBarServices barServices, ICocktailServices cocktailServices)
        {
            _barServices = barServices;
            _cocktailServices = cocktailServices;
        }

        public async Task<IActionResult> Index()
        {
            var bars = (await _barServices.LoadNewestBars()).Select(b => b.MapToViewModel()).ToList();
            var cocktails = (await _cocktailServices.LoadNewestCocktails()).Select(c => c.MapToViewModel()).ToList();
            var homeViewModel = new HomeViewModel
            {
                Bars = bars,
                Cocktails = cocktails,
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
