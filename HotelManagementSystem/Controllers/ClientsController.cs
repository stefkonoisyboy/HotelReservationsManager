using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Clients;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientsService clientsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ClientsController(IClientsService clientsService, UserManager<ApplicationUser> userManager)
        {
            this.clientsService = clientsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.EmployeeRole)]
        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.EmployeeRole)]
        public async Task<IActionResult> Create(CreateClientInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            int clientId = await this.clientsService.CreateAsync(inputModel);
            this.TempData["Message"] = "Client Create Successfully";
            
            return this.RedirectToAction("All", "Clients");
        }

        public IActionResult ById(int id)
        {
            ClientViewModel? viewModel = this.clientsService.GetById(id);
            
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.EmployeeRole)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            try
            {
                await this.clientsService.DeleteAsync(id);
                this.TempData["Message"] = "Successfully deleted client!";
            }
            catch (Exception ex)
            {
                this.TempData["ErrorMessage"] = ex.Message;
            }

            return this.RedirectToAction("All","Clients");
        }

        [Authorize(Roles = GlobalConstants.EmployeeRole)]
        public async Task<IActionResult> UpdateClient(int id)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            UpdateClientInputModel inputModel = new UpdateClientInputModel();
            try
            {
                ClientViewModel client = this.clientsService.GetById(id);
                inputModel = new UpdateClientInputModel()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    PhoneNumber = client.PhoneNumber,
                    Email = client.Email,
                    IsAdult = client.IsAdult
                };
            }
            catch (Exception ex)
            {
                this.TempData["ErrorMessage"] = ex.Message;
                return this.RedirectToAction("Index", "Home");
            }

            
            return this.View(inputModel);
        }

        [Authorize(Roles = GlobalConstants.EmployeeRole)]
        [HttpPost]
        public async Task<IActionResult> UpdateClient(UpdateClientInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user.DismissalDate != null)
            {
                return LocalRedirect("/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.clientsService.UpdateAsync(inputModel);
            this.TempData["Message"] = "Successfully updated client!";
            return this.RedirectToAction("All", "Clients");
        }

        
        public async Task<IActionResult> All(int page = 1)
        {
            const int itemsPerPage = 5;
            AllClientsListViewModel viewModel = new AllClientsListViewModel()
            {
                PageNumber = page,
                ItemsPerPage = itemsPerPage,
                ItemsCount = this.clientsService.GetClientsCount(),
                Clients = await this.clientsService.GetAllAsync(page,itemsPerPage),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AllFilteredByFirstNameAndLastName(FirstNameAndLastNameInputModel inputModel, int page=1)
        {
            const int itemsPerpage = 5;
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All", "Clients");
            }

            try
            {
                AllClientsListViewModel viewModel = new AllClientsListViewModel()
                {
                    Clients = await this.clientsService.FilterByFirstNameAndLastName(inputModel, page, itemsPerpage),
                    ItemsCount = this.clientsService.GetFilteredClientsCount(inputModel),
                    ItemsPerPage = itemsPerpage,
                    PageNumber = page,
                    InputModel = inputModel,
                };

                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData["Error"] = "There was an error!";
                return this.RedirectToAction("All", "Clients");
            }
        }

       
    }
}
