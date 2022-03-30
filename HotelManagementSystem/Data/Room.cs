using HotelManagementSystem.Data.Enumerations;

namespace HotelManagementSystem.Data
{
    public class Room
    {
        public Room()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? MainImage { get; set; }

        public int Capacity { get; set; }

        public RoomType RoomType { get; set; }

        public bool IsFree { get; set; }

        public decimal AdultPrice { get; set; }

        public decimal ChildPrice { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel? Hotel { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
