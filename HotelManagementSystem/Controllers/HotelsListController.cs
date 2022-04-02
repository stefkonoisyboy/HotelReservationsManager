using HotelManagementSystem.Models.HotelsList;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class HotelsListController : Controller
    {
        private readonly IHotelsService hotelsService;
        public HotelsListController(IHotelsService hotelsService)
        {
            this.hotelsService = hotelsService;
        }

        public async Task<IActionResult> HotelsList()
        {
            this.ViewData["BestStarredHotels"] = await this.hotelsService.GetBestStarredHotelsList();
            this.ViewData["MostReviewedHotels"] = await this.hotelsService.GetMostReviewedHotelsList();
            this.ViewData["MostReviewsStarsHotels"] = await this.hotelsService.GetMostReviewsStarsHotelsList();
            this.ViewData["CheapestHotels"] = await this.hotelsService.CheapestHotelsList();

            return this.View();
        }
    }
}
