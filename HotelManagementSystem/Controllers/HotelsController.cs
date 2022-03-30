using HotelManagementSystem.Models.Hotels;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelsService hotelsService;

        public HotelsController(IHotelsService hotelsService)
        {
            this.hotelsService = hotelsService;
        }

        public async Task<IActionResult> Details(int id)
        {
            HotelsDetailsViewModel viewModel = await this.hotelsService.GetById(id);
            return this.View(viewModel);
        }
    }
}
