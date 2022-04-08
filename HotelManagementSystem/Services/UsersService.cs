using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbcontext;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public UsersService(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> _userManager,
            IUserStore<ApplicationUser> _userStore)
        {
            this.dbcontext = dbContext;
            this._userManager = _userManager;
            this._userStore = _userStore;
        }

        public bool CheckUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(CreateUserInputModel inputModel)
        {
            var user = Activator.CreateInstance<ApplicationUser>();

            await _userStore.SetUserNameAsync(user, inputModel.Username, CancellationToken.None);
            await _userManager.SetEmailAsync(user, inputModel.Email);

            user.Id = Guid.NewGuid().ToString();
            user.FirstName = inputModel.FirstName;
            user.MiddleName = inputModel.MiddleName;
            user.LastName = inputModel.LastName;
            user.EGN = inputModel.EGN;
            user.HireDate = inputModel.HireDate;
            user.ProfileImage = inputModel.ProfileImage;
            user.IsActive = true;

            var result = await _userManager.CreateAsync(user, inputModel.Password);
            
            await this._userManager.AddToRoleAsync(user, GlobalConstants.EmployeeRole);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User creation failed!");
            }

        }

        public async Task UpdateAsync(UpdateUserInputModel inputModel)
        {
            var user = await this._userManager.FindByIdAsync(inputModel.Id);

            if (user == null)
            {
                throw new InvalidOperationException("This user doesn't exist!");
            }

            user.FirstName = inputModel.FirstName;
            user.MiddleName = inputModel.MiddleName;
            user.LastName = inputModel.LastName;
            user.Email = inputModel.Email;
            user.UserName = inputModel.Username;
            user.EGN = inputModel.EGN;
            user.HireDate = inputModel.HireDate;
            user.ProfileImage = inputModel.ProfileImage;
            user.IsActive = inputModel.IsActive;

            if (inputModel.DismissalDate != null)
            {
                user.DismissalDate = inputModel.DismissalDate;
            }

            await this._userManager.ChangePasswordAsync(user, inputModel.CurrentPassword, inputModel.NewPassword);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new InvalidOperationException("This user doesn't exist!");
            }

            await this._userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAll(string criteria, int page, int itemsperpage = 10)
        {
            switch (criteria)
            {
                case "Username":
                    return await this.GetUsersByUsername().Skip((page - 1) * itemsperpage).Take(itemsperpage).Select(u => new AllUsersViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName,
                        Email = u.Email,
                    }).ToListAsync();

                case "Firstname":
                    return await this.GetUsersByFirstname().Skip((page - 1) * itemsperpage).Take(itemsperpage).Select(u => new AllUsersViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName,
                        Email = u.Email,
                    }).ToListAsync();

                case "Middlename":
                    return await this.GetUsersByMiddlename().Skip((page - 1) * itemsperpage).Take(itemsperpage).Select(u => new AllUsersViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName,
                        Email = u.Email,
                    }).ToListAsync();

                case "Lastname":
                    return await this.GetUsersByLastname().Skip((page - 1) * itemsperpage).Take(itemsperpage).Select(u => new AllUsersViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName,
                        Email = u.Email,
                    }).ToListAsync();

                case "Email":
                    return await this.GetUsersByEmail().Skip((page - 1) * itemsperpage).Take(itemsperpage).Select(u => new AllUsersViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName,
                        Email = u.Email,
                    }).ToListAsync();

                default:
                    return await this._userManager.Users.Skip((page - 1) * itemsperpage).Take(itemsperpage).Select(u => new AllUsersViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName,
                        Email = u.Email,
                    }).ToListAsync();
            };
        }

        public int GetUsersCount()
        {
            return this._userManager.Users.Count();
        }

        public IQueryable<ApplicationUser> GetUsersByUsername()
        {
            return this._userManager.Users.OrderBy(u => u.UserName);
        }

        public IQueryable<ApplicationUser> GetUsersByFirstname()
        {
            return this._userManager.Users.OrderBy(u => u.FirstName);
        }

        public IQueryable<ApplicationUser> GetUsersByMiddlename()
        {
            return this._userManager.Users.OrderBy(u => u.MiddleName);
        }

        public IQueryable<ApplicationUser> GetUsersByLastname()
        {
            return this._userManager.Users.OrderBy(u => u.LastName);
        }

        public IQueryable<ApplicationUser> GetUsersByEmail()
        {
            return this._userManager.Users.OrderBy(u => u.Email);
        }

        public async Task<UpdateUserInputModel> GetByIdForUpdate(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new InvalidOperationException("This user doesn't exist!");
            }

            return new UpdateUserInputModel
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                EGN = user.EGN,
                HireDate = user.HireDate,
                ProfileImage = user.ProfileImage,
                IsActive = user.IsActive,
                DismissalDate = user.DismissalDate,
            };
        }
    }
}
