using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Reservations;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationsService reservationsService;
        private readonly IClientsService clientsService;
        private readonly IRoomsService roomsService;
        private readonly IHotelsService hotelsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReservationsController(
            IReservationsService reservationsService,
            IClientsService clientsService,
            IRoomsService roomsService,
            IHotelsService hotelsService,
            UserManager<ApplicationUser> userManager)
        {
            this.reservationsService = reservationsService;
            this.clientsService = clientsService;
            this.roomsService = roomsService;
            this.hotelsService = hotelsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Details(int id)
        {
            ReservationViewModel viewModel = await this.reservationsService.GetByIdAsync(id);
            return this.View(viewModel);
        }

        public async Task<IActionResult> All(int id = 1)
        {
            const int ItemsPerPage = 10;

            AllReservationsListViewModel viewModel = new AllReservationsListViewModel
            {
                PageNumber = id,
                ItemsPerPage = ItemsPerPage,
                ItemsCount = this.reservationsService.GetAllReservationsCount(),
                Reservations = await this.reservationsService.GetAllAsync(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Create(int roomId)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            CreateReservationInputModel input = new CreateReservationInputModel
            {
                ClientItems = await this.clientsService.GetAllAsSelectListItemsAsync(),
                AccommodationDate = DateTime.UtcNow,
                ExemptionDate = DateTime.UtcNow.AddDays(2),
            };

            return this.View(input);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(int roomId, CreateReservationInputModel input)
        {
            var user2 = await this.userManager.GetUserAsync(HttpContext.User);
            if (user2.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                input.ClientItems = await this.clientsService.GetAllAsSelectListItemsAsync();
                input.RoomId = roomId;

                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            input.RoomId = roomId;
            input.CreatorId = userId;

            try
            {
                await this.reservationsService.CreateAsync(roomId, userId, input);
                this.TempData["Message"] = "Reservation created successfully!";
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            EditReservationInputModel input = await this.reservationsService.GetByIdForUpdateAsync(id);
            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditReservationInputModel input)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.reservationsService.UpdateAsync(input);
                this.TempData["Message"] = "Reservation successfully updated!";
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            await this.reservationsService.DeleteAsync(id);
            this.TempData["Message"] = "Reservation successfully deleted!";

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> DeleteAllExpired()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            await this.reservationsService.DeleteAllExpiredAsync();
            this.TempData["Message"] = "Rooms successfully emptied!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
