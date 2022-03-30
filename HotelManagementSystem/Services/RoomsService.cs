using HotelManagementSystem.Data;

namespace HotelManagementSystem.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly ApplicationDbContext dbContext;

        public RoomsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string GetRoomName(int id)
        {
            return this.dbContext.Rooms
                .FirstOrDefault(r => r.Id == id)
                .Name;
        }
    }
}
