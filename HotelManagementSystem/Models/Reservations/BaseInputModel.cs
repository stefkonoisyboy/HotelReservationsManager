namespace HotelManagementSystem.Models.Reservations
{
    public class BaseInputModel
    {
        public int RoomId { get; set; }

        public string? CreatorId { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime ExemptionDate { get; set; }

        public string IsBreakfastIncluded { get; set; }

        public string IsAllInclusive { get; set; }
    }
}
