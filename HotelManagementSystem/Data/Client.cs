namespace HotelManagementSystem.Data
{
    public class Client
    {
        public Client()
        {
            this.Reservations = new HashSet<ClientReservation>();
        }

        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public bool IsAdult { get; set; }

        public virtual ICollection<ClientReservation> Reservations { get; set; }
    }
}
