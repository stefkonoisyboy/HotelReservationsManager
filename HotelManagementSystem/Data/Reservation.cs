namespace HotelManagementSystem.Data
{
    public class Reservation
    {
        public Reservation()
        {
            this.Clients = new HashSet<ClientReservation>();
        }

        public int Id { get; set; }

        public int ReservedRoomId { get; set; }

        public virtual Room? ReservedRoom { get; set; }

        public string? CreatorId { get; set; }

        public ApplicationUser? Creator { get; set; }

        public virtual ICollection<ClientReservation> Clients { get; set; }

        public DateTime AccomodationDate { get; set; }

        public DateTime ExemptionDate { get; set; }

        public bool IsBreakfastIncluded { get; set; }

        public bool IsAllInclusive { get; set; }

        public decimal GeneralAmount { get; set; }
    }
}
