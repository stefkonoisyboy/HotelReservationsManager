using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Clients;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientsService clientsService;

        public ClientsController(IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        //[Authorize(Roles = "ApplicationUser")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        //[Authorize(Roles = "ApplicationUser")]
        public async Task<IActionResult> Create(CreateClientInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            int clientId = await this.clientsService.CreateAsync(inputModel);
            this.TempData["Message"] = "Client Create Successfully";
            
            return this.RedirectToAction("All", "Clients");
        }

        //[Authorize(Roles = "ApplicationUser")]
        public IActionResult ById(int id)
        {
            ClientViewModel? viewModel = this.clientsService.GetById(id);
            
            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
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

        public IActionResult UpdateClient(int id)
        {
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

        //[Authorize(Roles="ApplicationUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateClient(UpdateClientInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.clientsService.UpdateAsync(inputModel);
            this.TempData["Message"] = "Successfully updated client!";
            return this.RedirectToAction("All", "Clients");
        }

        public IActionResult All()
        {
            AllClientsListViewModel viewModel = new AllClientsListViewModel()
            {
                Clients = this.clientsService.GetAll(),
            };

            return this.View(viewModel);
        }
    }
}
