namespace HotelManagementSystem.Models.Reservations
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public string? RoomName { get; set; }

        public string? HotelName { get; set; }

        public string? PhotoRemoteUrl { get; set; }

        public decimal GeneralAmount { get; set; }

        public int ClientsCount { get; set; }
    }
}
