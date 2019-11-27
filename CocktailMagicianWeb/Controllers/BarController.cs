using CocktailMagician.Services.Contracts;
using CocktailMagicianWeb.Models;
using CocktailMagicianWeb.Models.Cocktails;
using CocktailMagicianWeb.Utilities.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Controllers
{
    public class BarController : Controller
    {
        private readonly IBarServices _barServices;
        private readonly ICocktailServices _cocktailServices;
        private readonly IBarCocktailServices _barCocktailServices;
        private readonly IApiServices _apiServices;
        private readonly IFormattingService _formattingService;
        private readonly IMailServices _mailServices;

        public BarController(IBarServices barServices, ICocktailServices cocktailServices, IBarCocktailServices barCocktailServices, IApiServices apiServices, IFormattingService formattingService, IMailServices mailServices)
        {
            _barServices = barServices;
            _cocktailServices = cocktailServices;
            _barCocktailServices = barCocktailServices;
            _apiServices = apiServices;
            _formattingService = formattingService;
            _mailServices = mailServices;
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
            if (await _barServices.BarWithThatNameExists(barViewModel.Name))
            {
                ModelState.AddModelError(string.Empty, "Bar with that name already exists.");
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var query = barViewModel.FormatApiTemplate(barViewModel.Address.Split(' '));
            var apiResult = await this._apiServices.CallApiAsync(query);
            barViewModel.ParseApiResult(apiResult);
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
            var viewModel = new BarViewModel(bar);
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

        public async Task<IActionResult> NameExists(string name)
        {
            var boolCheck = await _barServices.BarWithThatNameExists(name);
            return Json(boolCheck);
        }
        public async Task HideBar([FromBody]BarViewModel vm)
        {
            await _barServices.HideAsync(vm.Id);
        }

        public async Task UnhideBar([FromBody]BarViewModel vm)
        {
            await _barServices.UnhideAsync(vm.Id);
        }
        [HttpGet]
        public async Task<IActionResult> GetBarEventPartial(int id)
        {
            var bar = await _barServices.GetBarAsync(id);
            return PartialView("_CreateEventPartial", new EventViewModel(bar));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBarEvent(EventViewModel model)
        {
            string modelAsString = string.Empty;

            var bar = await _barServices.GetBarAsync(model.BarId);
            var cocktailViewModel = new CocktailsViewModel();
            cocktailViewModel.Cocktails = bar.BarCocktails.Select(bc => bc.Cocktail.MapToViewModel()).ToList();
            try
            {
                model.BarViewHtmlString = await this._formattingService.RenderViewToStringAsync<CocktailsViewModel>("_BarMenuPartial",cocktailViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var emails = model.InvitationEmailAddresses.TrimEnd('x').Split(new char[] { 'x', ',' }, StringSplitOptions.None);
            var from = "vesselinignatoff88@gmail.com";
            var body = model.ToString();
            var subject = "Event invitation";
            var viewAsPdf = this._formattingService.HtmlStringToPDF(model.BarViewHtmlString);

            bool mailsSent = await this._mailServices.SendEmailToGroup(from, emails, subject, body, viewAsPdf);

            // тука може да си добавите сървис, който да запазва ивента в базата. в момента нямам имплементиран такъв сървис, нито имам ентити за ивент, има само ивентвюмодел
            // може в джейсъна да сложите и още пропъртита за да има за всяка операция.



            // bool successfullyCreated = this.eventServices.CreateAsync(model);  => псевдо код как запазвате ивента в базата и ако е успешно може да върнете от сървиса тру, иначе фолс и
            // така може да пратите инфо в джейсъна за всичките неща - и за статуса на мейлите и за статуса на самия ивент

            return Json(new { mailsSent = mailsSent/*, successfullyCreated = successfullyCreated*/ });

        }
    }
}