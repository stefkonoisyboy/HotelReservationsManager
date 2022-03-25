using HotelManagementSystem.Models;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelsService hotelsService;

        public HomeController(ILogger<HomeController> logger, IHotelsService hotelsService)
        {
            _logger = logger;
            this.hotelsService = hotelsService;
        }

        public async Task<IActionResult> Index()
        {
            this.ViewData["RecommendedHotels"] = await this.hotelsService.RecommendedHotels();
            this.ViewData["TravelersChoiceHotels"] = await this.hotelsService.TravelersChoiceHotels();

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}