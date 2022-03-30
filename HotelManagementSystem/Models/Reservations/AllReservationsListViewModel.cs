using HotelManagementSystem.Models.Paging;

namespace HotelManagementSystem.Models.Reservations
{
    public class AllReservationsListViewModel : PagingViewModel
    {
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}
