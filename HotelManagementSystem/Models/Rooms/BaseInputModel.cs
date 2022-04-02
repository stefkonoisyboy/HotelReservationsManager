using HotelManagementSystem.Data.Enumerations;

namespace HotelManagementSystem.Models.Rooms
{
    public abstract class BaseInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string MainImage { get; set; }

        public List<string> RoomTypes => Enum.GetNames(typeof(RoomType)).ToList();

        public string RoomType { get; set; }

        public bool IsFree { get; set; }

        public int Capacity { get; set; }

        public decimal AdultPrice { get; set; }

        public decimal ChildPrice { get; set; }

        public int HotelId { get; set; }
    }
}
