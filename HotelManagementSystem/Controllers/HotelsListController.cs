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
            this.ViewData["HotelsList"] = await this.hotelsService.GetHotelsList();
            return this.View();
        }
    }
}
