using HotelManagementSystem.Models.Users;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await this.usersService.CreateAsync(inputModel);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, [FromQuery]string criteria = null)
        {
            IEnumerable<AllUsersViewModel> allUsers = await this.usersService.GetAll(criteria, page, 10);

            AllUsersListViewModel allUsersListViewModel = new AllUsersListViewModel
            {
                Users = allUsers,
                ItemsCount = this.usersService.GetUsersCount(),
                PageNumber = page,
                ItemsPerPage = 10,
            };

            return this.View(allUsersListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            UpdateUserInputModel inputModel = await this.usersService.GetByIdForUpdate(id);

            return this.View(inputModel);
        }

        public async Task<IActionResult> Update(UpdateUserInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await this.usersService.UpdateAsync(inputModel);

            return this.Redirect("/Users/GetAll");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.usersService.DeleteAsync(id);

            return this.Redirect("/Users/GetAll");
        }
    }
}
