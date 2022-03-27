namespace HotelManagementSystem.Data
{
    public class ClientReservation
    {
        public int Id { get; set; }

        public  int ReservationId { get; set; }

        public virtual Reservation? Reservation { get; set; }

        public int ClientId { get; set; }

        public Client? Client { get; set; }
    }
}
