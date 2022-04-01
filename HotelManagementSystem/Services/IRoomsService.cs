using HotelManagementSystem.Models.Rooms;

namespace HotelManagementSystem.Services
{
    public interface IRoomsService
    {
        string GetRoomName(int id);

        Task CreateAsync(CreateRoomInputModel inputModel);

        Task DeleteAsync(int id);

        Task UpdateAsync(UpdateRoomInputModel inptuModel);

        UpdateRoomInputModel GetByIdForUpdate(int id);

    }
}
