namespace HotelManagementSystem.Models.Clients
{
    public class AllClientsViewModel
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public int ReservationsCount { get; set; }
    }
}
