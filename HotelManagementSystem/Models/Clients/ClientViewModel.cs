using HotelManagementSystem.Models.Reservations;

namespace HotelManagementSystem.Models.Clients
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public bool IsAdult { get; set; }

        public ICollection<ReservationViewModel>? Reservations { get; set; }
    }
}
