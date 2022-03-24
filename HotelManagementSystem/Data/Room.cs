using HotelManagementSystem.Data.Enumerations;

namespace HotelManagementSystem.Data
{
    public class Room
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Capacity { get; set; }

        public RoomType RoomType { get; set; }

        public bool IsFree { get; set; }

        public decimal AdultPrice { get; set; }

        public decimal ChildPrice { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel? Hotel { get; set; }
    }
}
