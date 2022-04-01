using HotelManagementSystem.Models.Rooms;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomsService roomsService;
        private readonly IHotelsService hotelsService;

        public RoomsController(IRoomsService roomsService, IHotelsService hotelsService)
        {
            this.roomsService = roomsService;
            this.hotelsService = hotelsService;
        }

        [HttpPost]
        [Authorize(Roles= GlobalConstants.AdministratorRole)]
        public async Task<IActionResult> Create(CreateRoomInputModel inputModel, int hotelid)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }
            inputModel.HotelId = hotelid;
            await this.roomsService.CreateAsync(inputModel);
            return this.RedirectToAction("Details", "Hotels", new { Id = inputModel.HotelId }); 
        }


        public IActionResult Update(int id)
        {
            UpdateRoomInputModel inputModel= this.roomsService.GetByIdForUpdate(id);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRole)]
        public async Task<IActionResult> Update(UpdateRoomInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.roomsService.UpdateAsync(inputModel);
            this.TempData["Message"] = "Successfully updated room.";
            return this.RedirectToAction("Details", "Hotels");
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.roomsService.DeleteAsync(id);
                this.TempData["Message"] = "Successfully delete room.";
            }
            catch (Exception ex)
            {
                this.TempData["ErrorMessage"] = ex.Message;
            }

            return this.RedirectToAction("Details", "Hotels");
        }
    }
}
