using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Users;

namespace HotelManagementSystem.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbcontext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbcontext = dbContext;
        }

        public bool CheckUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(CreateUserInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateUserInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AllUsersViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
