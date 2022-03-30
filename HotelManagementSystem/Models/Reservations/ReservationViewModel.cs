using HotelManagementSystem.Models.Clients;

namespace HotelManagementSystem.Models.Reservations
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public string? RoomName { get; set; }

        public string? HotelName { get; set; }

        public string? PhotoRemoteUrl { get; set; }

        public decimal GeneralAmount { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime ExemptionDate { get; set; }

        public string IsBreakfastIncluded { get; set; }

        public string IsAllInclusive { get; set; }

        public int ClientsCount { get; set; }

        public IEnumerable<AllClientsByReservationViewModel> Clients { get; set; }
    }
}
