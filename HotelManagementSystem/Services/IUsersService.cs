using HotelManagementSystem.Models.Users;

namespace HotelManagementSystem.Services
{

    public interface IUsersService
    {

        bool CheckUser(string userId);

        Task CreateAsync(CreateUserInputModel inputModel);

        Task UpdateAsync(UpdateUserInputModel inputModel);

        Task DeleteAsync(string id);

        IEnumerable<AllUsersViewModel> GetAll();
    }
}
