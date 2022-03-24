using HotelManagementSystem.Models.SearchHotels;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class SearchHotelsController : Controller
    {
        private readonly IHotelsService hotelsService;

        public SearchHotelsController(IHotelsService hotelsService)
        {
            this.hotelsService = hotelsService;
        }

        public async Task<IActionResult> List()
        {
            SearchHotelListInputModel input = new SearchHotelListInputModel
            {
                Hotels = await this.hotelsService.GetAll(),
            };

            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> List(SearchHotelListInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                input.Hotels = await this.hotelsService.Search(input);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
            }

            return this.View(input);
        }
    }
}
