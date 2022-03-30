using HotelManagementSystem.Models.Reservations;

namespace HotelManagementSystem.Services
{
    public interface IReservationsService
    {
        Task CreateAsync(CreateReservationInputModel input);

        Task UpdateAsync(EditReservationInputModel input);

        Task DeleteAsync(int id);

        int GetAllReservationsCount();

        Task<ReservationViewModel> GetByIdAsync(int id);

        Task<EditReservationInputModel> GetByIdForUpdateAsync(int id);

        Task<IEnumerable<ReservationViewModel>> GetAllAsync(int page, int itemsPerPage = 10);
    }
}
