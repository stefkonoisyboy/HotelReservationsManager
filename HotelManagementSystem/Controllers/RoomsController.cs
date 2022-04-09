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

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public async Task<IActionResult> Create() 
        {
            CreateRoomInputModel inputModel = new CreateRoomInputModel()
            {
                HotelItems = await this.hotelsService.GetHotelsAsSelectListItem(),
            };

            return this.View(inputModel);
        }


        [HttpPost]
        [Authorize(Roles= GlobalConstants.AdministratorRole)]
        public async Task<IActionResult> Create(CreateRoomInputModel inputModel, int hotelid)
        {
            if (!ModelState.IsValid)
            {
                inputModel.HotelItems = await this.hotelsService.GetHotelsAsSelectListItem();
                return this.View(inputModel);
            }

            inputModel.HotelId = hotelid;
            await this.roomsService.CreateAsync(inputModel);
            return this.RedirectToAction("Details", "Hotels", new { Id = inputModel.HotelId }); 
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public async Task<IActionResult> Update(int id)
        {
            UpdateRoomInputModel inputModel= this.roomsService.GetByIdForUpdate(id);
            inputModel.HotelItems = await this.hotelsService.GetHotelsAsSelectListItem();

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRole)]
        public async Task<IActionResult> Update(UpdateRoomInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.HotelItems = await this.hotelsService.GetHotelsAsSelectListItem();
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

        public async Task<IActionResult> All(int page = 1)
        {
            const int itemsPerPage = 5;
            AllRoomsListViewModel viewModel = new AllRoomsListViewModel()
            {
                Rooms = await this.roomsService.GetAll(page),
                ItemsCount = this.roomsService.GetRoomsCount(),
                PageNumber = page,
                ItemsPerPage = itemsPerPage,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AllFiltered(FilterRoomsInputModel inputModel, int page = 1)
        {
            const int itemsPerPage = 5;

            AllRoomsListViewModel viewModel = new AllRoomsListViewModel()
            {
                Rooms = await this.roomsService.GetAllFiltered(page, inputModel),
                ItemsCount = this.roomsService.GetFilteredRoomsCount(inputModel),
                PageNumber = page,
                ItemsPerPage = itemsPerPage,
            };

            viewModel.InputModel = inputModel;

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            try
            {
                RoomDetailsViewModel viewModel = this.roomsService.GetById(id);
                return this.View(viewModel);

            }
            catch (Exception ex)
            {
                this.TempData["ErrorMessage"] = "Somethind went wrong!";
                return this.RedirectToAction("Index","Home");
            }
        }
    }
}
