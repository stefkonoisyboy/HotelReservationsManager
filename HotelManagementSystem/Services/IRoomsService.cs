using HotelManagementSystem.Models.Rooms;

namespace HotelManagementSystem.Services
{
    public interface IRoomsService
    {
        string GetRoomName(int id);

        int GetRoomsCount();

        int GetFilteredRoomsCount(FilterRoomsInputModel inputModel);

        Task CreateAsync(CreateRoomInputModel inputModel);

        Task DeleteAsync(int id);

        Task UpdateAsync(UpdateRoomInputModel inptuModel);

        UpdateRoomInputModel GetByIdForUpdate(int id);

        Task<IEnumerable<AllRoomsViewModel>> GetAll(int page, int itemsPerPage = 5);

        Task<IEnumerable<AllRoomsViewModel>> GetAllFiltered(int page, FilterRoomsInputModel inputModel, int itemsPerPage = 5);

    }
}
