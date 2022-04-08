using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Users;

namespace HotelManagementSystem.Services
{

    public interface IUsersService
    {

        bool CheckUser(string userId);

        Task CreateAsync(CreateUserInputModel inputModel);

        Task UpdateAsync(UpdateUserInputModel inputModel);

        Task DeleteAsync(string id);

        Task<IEnumerable<AllUsersViewModel>> GetAll(string criteria, int page, int itemsperpage);

        Task<UpdateUserInputModel> GetByIdForUpdate(string id);

        IQueryable<ApplicationUser> GetUsersByUsername();

        IQueryable<ApplicationUser> GetUsersByFirstname();

        IQueryable<ApplicationUser> GetUsersByMiddlename();

        IQueryable<ApplicationUser> GetUsersByLastname();

        IQueryable<ApplicationUser> GetUsersByEmail();

        int GetUsersCount();
    }
}
